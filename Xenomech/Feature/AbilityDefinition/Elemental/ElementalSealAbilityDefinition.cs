using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.Elemental
{
    public class ElementalSealAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ElementalSeal();
            
            return _builder.Build();
        }

        private void ElementalSeal()
        {
            _builder.Create(FeatType.ElementalSeal, PerkType.ElementalSeal)
                .Name("Elemental Seal")
                .HasRecastDelay(RecastGroup.ElementalSeal, 300f)
                .IsCastedAbility()
                .UsesAnimation(Animation.LoopingConjure2)
                .HasImpactAction((activator, _, _, targetLocation) =>
                {
                    StatusEffect.Apply(activator, activator, StatusEffectType.ElementalSeal, 30f);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Good_Help), activator);

                    Enmity.ModifyEnmityOnAll(activator, 30);
                });
        }
    }
}
