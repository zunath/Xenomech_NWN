using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;

namespace Xenomech.Service.AbilityService
{
    public class ActiveConcentrationAbility
    {
        public ActiveConcentrationAbility(FeatType feat, StatusEffectType statusEffectType)
        {
            Feat = feat;
            StatusEffectType = statusEffectType;
        }

        public FeatType Feat { get; set; }
        public StatusEffectType StatusEffectType { get; set; }
    }
}
