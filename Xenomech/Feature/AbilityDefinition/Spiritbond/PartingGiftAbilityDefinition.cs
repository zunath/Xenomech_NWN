using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.Creature;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;
using Random = Xenomech.Service.Random;

namespace Xenomech.Feature.AbilityDefinition.Spiritbond
{
    public class PartingGiftAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            PartingGift();

            return _builder.Build();
        }

        private void ImpactActionFaith(uint activator, int baseAmount)
        {
            var activatorSpirit = GetAbilityModifier(AbilityType.Spirit, activator);
            var maxBonus = activatorSpirit * 10;
            var minBonus = activatorSpirit * 5;

            foreach (var member in Party.GetAllPartyMembersWithinRange(activator, 8f))
            {
                var amount = baseAmount + Random.Next(minBonus, maxBonus);

                ApplyEffectToObject(DurationType.Instant, EffectHeal(amount), member);
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Healing_L), member);
                Enmity.ModifyEnmityOnAll(activator, amount + 10);
            }

            Enmity.ModifyEnmityOnAll(activator, 40);
            Ability.EndConcentrationAbility(activator);
        }

        private void ImpactActionChange(uint activator, StatusEffectType type)
        {
            var activatorSpirit = GetAbilityModifier(AbilityType.Spirit, activator);

            foreach (var member in Party.GetAllPartyMembersWithinRange(activator, 8f))
            {
                var length = 300f + activatorSpirit * 10f;
                
                StatusEffect.Apply(activator, member, type, length);
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Ac_Bonus), member);
                Enmity.ModifyEnmityOnAll(activator, 10);
            }

            Enmity.ModifyEnmityOnAll(activator, 40);
            Ability.EndConcentrationAbility(activator);
        }

        private void ImpactActionRage(uint activator, float dmg)
        {
            var count = 1;
            var nearby = GetNearestCreature(CreatureType.IsAlive, 1, activator, count);
            var attackerSpirit = GetAbilityModifier(AbilityType.Spirit, activator);

            while (GetIsObjectValid(nearby) &&
                   GetDistanceBetween(activator, nearby) <= 8f &&
                   count <= 10)
            {
                var defenderSpirit = GetAbilityModifier(AbilityType.Spirit, nearby);
                var defenderEDEF = Combat.CalculateEtherDefense(nearby);
                var damage = Combat.CalculateDamage(dmg, attackerSpirit, defenderEDEF, defenderSpirit, false);

                var nearbyCopy = nearby;
                AssignCommand(activator, () =>
                {
                    ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Electrical), nearbyCopy);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Doom), nearbyCopy);
                });

                count++;
                nearby = GetNearestCreature(CreatureType.IsAlive, 1, activator, count);
            }

            Ability.EndConcentrationAbility(activator);
        }

        private void PartingGift()
        {
            _builder.Create(FeatType.PartingGift, PerkType.PartingGift)
                .Name("Parting Gift")
                .HasRecastDelay(RecastGroup.PartingGift, 120f)
                .IsCastedAbility()
                .UsesAnimation(Animation.LoopingConjure2)
                .HasImpactAction((activator, _, _, targetLocation) =>
                {
                    if (StatusEffect.HasStatusEffect(activator, StatusEffectType.SpiritOfFaith1))
                    {
                        ImpactActionFaith(activator, 40);
                    }
                    else if (StatusEffect.HasStatusEffect(activator, StatusEffectType.SpiritOfFaith2))
                    {
                        ImpactActionFaith(activator, 100);
                    }
                    else if (StatusEffect.HasStatusEffect(activator, StatusEffectType.SpiritOfFaith3))
                    {
                        ImpactActionFaith(activator, 160);
                    }

                    else if (StatusEffect.HasStatusEffect(activator, StatusEffectType.SpiritOfChange1))
                    {
                        ImpactActionChange(activator, StatusEffectType.SpiritProtection1);
                    }
                    else if (StatusEffect.HasStatusEffect(activator, StatusEffectType.SpiritOfChange2))
                    {
                        ImpactActionChange(activator, StatusEffectType.SpiritProtection2);
                    }
                    else if (StatusEffect.HasStatusEffect(activator, StatusEffectType.SpiritOfChange3))
                    {
                        ImpactActionChange(activator, StatusEffectType.SpiritProtection3);
                    }

                    else if (StatusEffect.HasStatusEffect(activator, StatusEffectType.SpiritOfRage1))
                    {
                        ImpactActionRage(activator, 10f);
                    }
                    else if (StatusEffect.HasStatusEffect(activator, StatusEffectType.SpiritOfRage2))
                    {
                        ImpactActionRage(activator, 16f);
                    }
                    else if (StatusEffect.HasStatusEffect(activator, StatusEffectType.SpiritOfRage3))
                    {
                        ImpactActionRage(activator, 21f);
                    }

                });
        }
    }
}
