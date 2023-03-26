using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MspLib.Class
{
    public class ActorDetails
    {
        public int ActorId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsMale { get; set; }
        public string SkinColor { get; set; }
        public int NoseId { get; set; }
        public int EyeId { get; set; }
        public int MouthId { get; set; }
        public double Money { get; set; }
        public string EyeColors { get; set; }
        public string MouthColors { get; set; }
        public double Fame { get; set; }
        public double Fortune { get; set; }
        public int FriendCount { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsModerator { get; set; }
        public long ProfileDisplays { get; set; }
        public bool IsExtra { get; set; }
        public int InvitedByActorId { get; set; }
        public int NumberOfGiftsGiven { get; set; }
        public int NumberOfGiftsReceived { get; set; }
        public int NumberOfAutographsReceived { get; set; }
        public int NumberOfAutographsGiven { get; set; }
        public DateTime TimeOfLastAutographGiven { get; set; }
        public DateTime MembershipPurchasedDate { get; set; }
        public DateTime MembershipTimeoutDate { get; set; }
        public DateTime LockedUntil { get; set; }
        public string LockedText { get; set; }
        public int FriendCountVIP { get; set; }
        public double Diamonds { get; set; }
        public string Email { get; set; }
    }
}
