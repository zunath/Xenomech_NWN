using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.Arcane
{
    public class ArcaneSpreadAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ArcaneSpread();

            return _builder.Build();
        }

        private void ArcaneSpread()
        {
            _builder.Create(FeatType.ArcaneSpread, PerkType.ArcaneSpread)
                .Name("Arcane Spread")
                .HasRecastDelay(RecastGroup.ArcaneSpread, 30f)
                .IsCastedAbility()
                .UsesAnimation(Animation.LoopingConjure2)
                .HasImpactAction((activator, _, _, targetLocation) =>
                {
                    StatusEffect.Apply(activator, activator, StatusEffectType.ArcaneSpread, 15f);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Evil_Help), activator);
                });
        }

    }
}
