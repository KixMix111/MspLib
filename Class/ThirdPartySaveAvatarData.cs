using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MspLib.Class
{
    public class ThirdPartySaveAvatarData
    {
        public string ChosenActorName { get; set; }
        public string ChosenPassword { get; set; }
        public bool SkinIsMale { get; set; }
        public object[] Clothes { get; set; }
        public int InvitedByActorId { get; set; }
        public string EyeColors { get; set; }
        public int EyeId { get; set; }
        public string SkinColor { get; set; }
        public string MouthColors { get; set; }
        public int MouthId { get; set; }
        public int NoseId { get; set; }

        public static ThirdPartySaveAvatarData BuildThirdPartySaveAvatar(string ChosenActorName, string ChosenPassword, bool SkinIsMale, List<Clothes> Clothes, string EyeColors, int EyeId, string SkinColor, string MouthColors, int MouthId, int NoseId)
            => new ThirdPartySaveAvatarData()
            {
                ChosenActorName = ChosenActorName,
                ChosenPassword = ChosenPassword,
                SkinIsMale = SkinIsMale,
                Clothes = Clothes.ToArray(),
                InvitedByActorId = -1,
                EyeColors = EyeColors,
                EyeId = EyeId,
                SkinColor = SkinColor,
                MouthColors = MouthColors,
                MouthId = MouthId,
                NoseId = NoseId
            };
    }
}
