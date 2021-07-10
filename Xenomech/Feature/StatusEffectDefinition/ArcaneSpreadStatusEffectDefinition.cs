using System.Collections.Generic;
using Xenomech.Enumeration;
using Xenomech.Service.StatusEffectService;

namespace Xenomech.Feature.StatusEffectDefinition
{
    public class ArcaneSpreadStatusEffectDefinition: IStatusEffectListDefinition
    {
        private static readonly StatusEffectBuilder _builder = new StatusEffectBuilder();

        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            ArcaneSpread();

            return _builder.Build();
        }

        private void ArcaneSpread()
        {
            _builder.Create(StatusEffectType.ArcaneSpread)
                .EffectIcon(1) // todo icon
                .Name("Arcane Spread");
        }
    }
}
