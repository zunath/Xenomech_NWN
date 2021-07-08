using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Service.AbilityService;

namespace Xenomech.Feature.AbilityDefinition.Arcane
{
    public class RyokushoAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {

            return _builder.Build();
        }
    }
}
