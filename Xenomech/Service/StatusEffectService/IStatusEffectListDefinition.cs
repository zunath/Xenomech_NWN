using System.Collections.Generic;
using Xenomech.Enumeration;

namespace Xenomech.Service.StatusEffectService
{
    public interface IStatusEffectListDefinition
    {
        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects();
    }
}
