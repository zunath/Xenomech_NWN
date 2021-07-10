using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.Elemental
{
    public class ElementalSpreadAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ElementalSpread();

            return _builder.Build();
        }

        private void ElementalSpread()
        {
            _builder.Create(FeatType.ElementalSpread, PerkType.ElementalSpread)
                .Name("Elemental Spread")
                .HasRecastDelay(RecastGroup.ElementalSpread, 30f)
                .IsCastedAbility()
                .UsesAnimation(Animation.LoopingConjure2)
                .HasImpactAction((activator, _, _, targetLocation) =>
                {
                    StatusEffect.Apply(activator, activator, StatusEffectType.ElementalSpread, 15f);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Evil_Help), activator);

                    Enmity.ModifyEnmityOnAll(activator, 30);
                });
        }
    }
}
