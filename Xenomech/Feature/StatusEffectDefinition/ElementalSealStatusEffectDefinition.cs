using System.Collections.Generic;
using Xenomech.Enumeration;
using Xenomech.Service.StatusEffectService;

namespace Xenomech.Feature.StatusEffectDefinition
{
    public class ElementalSealStatusEffectDefinition: IStatusEffectListDefinition
    {
        private static readonly StatusEffectBuilder _builder = new StatusEffectBuilder();

        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            ElementalSeal();

            return _builder.Build();
        }

        private void ElementalSeal()
        {
            _builder.Create(StatusEffectType.ElementalSeal)
                .EffectIcon(1) // todo icon
                .Name("Elemental Seal");
        }

    }
}
