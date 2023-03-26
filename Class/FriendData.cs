using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MspLib.Class
{
    public class FriendData
    {
        public int ActorId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool RecentlyLoggedIn { get; set; }
        public DateTime MembershipTimeoutDate { get; set; }
        public DateTime LastInteractionDate { get; set; }
        public double Money { get; set; }
        public double Fame { get; set; }
        public double Fortune { get; set; }
        public int FriendCount { get; set; }
        public int RoomLikes { get; set; }
        public DateTime MembershipPurchasedDate { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsModerator { get; set; }
        public string NebulaProfileId { get; set; }
    }
}
