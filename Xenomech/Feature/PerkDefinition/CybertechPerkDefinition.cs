using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service.PerkService;

namespace Xenomech.Feature.PerkDefinition
{
    public class CybertechPerkDefinition : IPerkListDefinition
    {
        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            var builder = new PerkBuilder();

            return builder.Build();
        }
        
    }
}
