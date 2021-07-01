using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;

namespace Xenomech.Service.AbilityService
{
    public interface IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities();
    }
}
