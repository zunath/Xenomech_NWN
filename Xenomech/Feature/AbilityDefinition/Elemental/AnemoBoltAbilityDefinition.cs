using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.Creature;
using Xenomech.Core.NWScript.Enum.Item.Property;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;
using DamageType = Xenomech.Core.NWScript.Enum.DamageType;

namespace Xenomech.Feature.AbilityDefinition.Elemental
{
    public class AnemoBoltAbilityDefinition : IAbilityListDefinition
    {
        private static readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            AnemoBolt1();
            AnemoBolt2();
            AnemoBolt3();

            return _builder.Build();
        }

        private static void ImpactAction(uint activator, uint target, float dmg, int evasionLoss)
        {
            var attackerSpirit = GetAbilityModifier(AbilityType.Spirit, activator);
            var targets = new List<uint>();
            targets.Add(target);

            if (StatusEffect.HasStatusEffect(activator, StatusEffectType.ElementalSpread))
            {
                var count = 1;
                var nearby = GetNearestCreature(CreatureType.IsAlive, 1, target, count);
                while (GetIsObjectValid(nearby) &&
                       count <= 10 &&
                       GetDistanceBetween(target, nearby) <= 5f)
                {
                    if (nearby == target) continue;

                    targets.Add(nearby);

                    count++;
                    nearby = GetNearestCreature(CreatureType.IsAlive, 1, target, count);
                }
            }

            foreach (var creature in targets)
            {
                var defenderSpirit = GetAbilityModifier(AbilityType.Spirit, creature);
                var defenderEDEF = Combat.CalculateEtherDefense(creature);
                var damage = Combat.CalculateDamage(dmg, attackerSpirit, defenderEDEF, defenderSpirit, false);
                var duration = 20f;

                if (StatusEffect.HasStatusEffect(activator, StatusEffectType.ElementalSeal))
                {
                    damage *= 2;
                    duration *= 2;

                    StatusEffect.Remove(activator, StatusEffectType.ElementalSeal);
                }

                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Electrical), creature);
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Lightning_S), creature);
                ApplyEffectToObject(DurationType.Temporary, EffectACDecrease(evasionLoss, ArmorClassModiferType.Natural), creature, duration);
            }
        }

        private static void AnemoBolt1()
        {
            _builder.Create(FeatType.AnemoBolt1, PerkType.AnemoBolt)
                .Name("Anemo Bolt I")
                .HasRecastDelay(RecastGroup.AnemoBolt, 2f)
                .HasActivationDelay(1.0f)
                .RequirementEP(3)
                .IsCastedAbility()
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, _, targetLocation) =>
                {
                    ImpactAction(activator, target, 7.5f, 2);
                });
        }

        private static void AnemoBolt2()
        {
            _builder.Create(FeatType.AnemoBolt2, PerkType.AnemoBolt)
                .Name("Anemo Bolt II")
                .HasRecastDelay(RecastGroup.AnemoBolt, 3f)
                .HasActivationDelay(2.0f)
                .RequirementEP(5)
                .IsCastedAbility()
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, _, targetLocation) =>
                {
                    ImpactAction(activator, target, 12.5f, 3);
                });
        }

        private static void AnemoBolt3()
        {
            _builder.Create(FeatType.AnemoBolt3, PerkType.AnemoBolt)
                .Name("Anemo Bolt III")
                .HasRecastDelay(RecastGroup.AnemoBolt, 4f)
                .HasActivationDelay(3.0f)
                .RequirementEP(7)
                .IsCastedAbility()
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, _, targetLocation) =>
                {
                    ImpactAction(activator, target, 17.5f, 4);
                });
        }
    }
}
