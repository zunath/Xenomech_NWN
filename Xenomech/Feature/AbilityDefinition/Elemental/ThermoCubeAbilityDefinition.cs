using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.Elemental
{
    public class ThermoCubeAbilityDefinition : IAbilityListDefinition
    {
        private static readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ThermoCube1();
            ThermoCube2();
            ThermoCube3();

            return _builder.Build();
        }

        private static void ImpactAction(uint activator, uint target, int level, float dmg)
        {
            var attackerSpirit = GetAbilityModifier(AbilityType.Spirit, activator);
            var defenderSpirit = GetAbilityModifier(AbilityType.Spirit, target);
            var defenderEDEF = Combat.CalculateEtherDefense(target);

            var damage = Combat.CalculateDamage(dmg, attackerSpirit, defenderEDEF, defenderSpirit, false);

            if (StatusEffect.HasStatusEffect(activator, StatusEffectType.ElementalSeal))
            {
                damage *= 2;

                StatusEffect.Remove(activator, StatusEffectType.ElementalSeal);
            }

            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Fire), target);
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Com_Hit_Fire), target);
        }

        private static void ThermoCube1()
        {
            _builder.Create(FeatType.ThermoCube1, PerkType.ThermoCube)
                .Name("Thermo Cube I")
                .HasRecastDelay(RecastGroup.ThermoCube, 2f)
                .HasActivationDelay(1.0f)
                .RequirementEP(2)
                .IsCastedAbility()
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, level) =>
                {
                    ImpactAction(activator, target, level, 10f);
                });
        }

        private static void ThermoCube2()
        {
            _builder.Create(FeatType.ThermoCube2, PerkType.ThermoCube)
                .Name("Thermo Cube II")
                .HasRecastDelay(RecastGroup.ThermoCube, 2f)
                .HasActivationDelay(1.0f)
                .RequirementEP(4)
                .IsCastedAbility()
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, level) =>
                {
                    ImpactAction(activator, target, level, 16f);
                });
        }

        private static void ThermoCube3()
        {
            _builder.Create(FeatType.ThermoCube3, PerkType.ThermoCube)
                .Name("Thermo Cube III")
                .HasRecastDelay(RecastGroup.ThermoCube, 2f)
                .HasActivationDelay(1.0f)
                .RequirementEP(6)
                .IsCastedAbility()
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, level) =>
                {
                    ImpactAction(activator, target, level, 21f);
                });
        }
    }
}
