using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Service.AbilityService;

namespace Xenomech.Feature.AbilityDefinition.NPC
{
    public class RoarAbilityDefinition: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {

            return _builder.Build();
        }
    }
}
