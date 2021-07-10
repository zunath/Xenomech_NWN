using System.Collections.Generic;
using Xenomech.Enumeration;
using Xenomech.Service.StatusEffectService;

namespace Xenomech.Feature.StatusEffectDefinition
{
    public class ElementalSpreadStatusEffectDefinition : IStatusEffectListDefinition
    {
        private static readonly StatusEffectBuilder _builder = new StatusEffectBuilder();

        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            ElementalSpread();

            return _builder.Build();
        }

        private void ElementalSpread()
        {
            _builder.Create(StatusEffectType.ElementalSpread)
                .EffectIcon(1) // todo icon
                .Name("Elemental Spread");
        }

    }
}