using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.Item.Property;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;
using DamageType = Xenomech.Core.NWScript.Enum.DamageType;

namespace Xenomech.Feature.AbilityDefinition.Elemental
{
    public class AquaIceAbilityDefinition : IAbilityListDefinition
    {
        private static readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            AnemoBolt1();
            AnemoBolt2();
            AnemoBolt3();

            return _builder.Build();
        }

        private static void ImpactAction(uint activator, uint target, float dmg)
        {
            var attackerSpirit = GetAbilityModifier(AbilityType.Spirit, activator);
            var defenderSpirit = GetAbilityModifier(AbilityType.Spirit, target);
            var defenderEDEF = Combat.CalculateEtherDefense(target);
            var duration = 6f;

            var damage = Combat.CalculateDamage(dmg, attackerSpirit, defenderEDEF, defenderSpirit, false);

            if (StatusEffect.HasStatusEffect(activator, StatusEffectType.ElementalSeal))
            {
                duration *= 2;
                damage *= 2;

                StatusEffect.Remove(activator, StatusEffectType.ElementalSeal);
            }

            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Cold), target);
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Frost_L), target);
            ApplyEffectToObject(DurationType.Temporary, EffectSlow(), target, duration);
        }

        private static void AnemoBolt1()
        {
            _builder.Create(FeatType.AquaIce1, PerkType.AquaIce)
                .Name("Aqua Ice I")
                .HasRecastDelay(RecastGroup.AquaIce, 2f)
                .HasActivationDelay(2.0f)
                .RequirementEP(2)
                .IsCastedAbility()
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, _) =>
                {
                    ImpactAction(activator, target, 6.5f);
                });
        }

        private static void AnemoBolt2()
        {
            _builder.Create(FeatType.AquaIce2, PerkType.AquaIce)
                .Name("Aqua Ice II")
                .HasRecastDelay(RecastGroup.AquaIce, 2f)
                .HasActivationDelay(3.0f)
                .RequirementEP(4)
                .IsCastedAbility()
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, _) =>
                {
                    ImpactAction(activator, target, 11.5f);
                });
        }

        private static void AnemoBolt3()
        {
            _builder.Create(FeatType.AquaIce3, PerkType.AquaIce)
                .Name("Aqua Ice III")
                .HasRecastDelay(RecastGroup.AquaIce, 2f)
                .HasActivationDelay(4.0f)
                .RequirementEP(6)
                .IsCastedAbility()
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, _) =>
                {
                    ImpactAction(activator, target, 16.5f);
                });
        }
    }
}
