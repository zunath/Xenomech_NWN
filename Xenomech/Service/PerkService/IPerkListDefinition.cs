using System.Collections.Generic;
using Xenomech.Enumeration;

namespace Xenomech.Service.PerkService
{
    public interface IPerkListDefinition
    {
        public Dictionary<PerkType, PerkDetail> BuildPerks();
    }
}
