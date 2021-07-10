using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.Item.Property;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;
using DamageType = Xenomech.Core.NWScript.Enum.DamageType;

namespace Xenomech.Feature.AbilityDefinition.Arcane
{
    public class ArcaneSpikesAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ArcaneSpikes1();
            ArcaneSpikes2();
            ArcaneSpikes3();

            return _builder.Build();
        }

        private void ImpactAction(uint activator, uint target, int baseAmount)
        {
            var activatorSpirit = GetAbilityModifier(AbilityType.Spirit, activator);
            var length = 300f + activatorSpirit * 10f;

            // Target first always
            ApplyEffectToObject(DurationType.Temporary, EffectDamageShield(baseAmount, DamageBonus.DAMAGEBONUS_1d8, DamageType.Piercing), target, length);
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Bigbys_Forceful_Hand), target);
            Enmity.ModifyEnmityOnAll(activator, 20);

            // If Arcane Spread is active, target nearby party members.
            if (StatusEffect.HasStatusEffect(activator, StatusEffectType.ArcaneSpread))
            {
                foreach (var member in Party.GetAllPartyMembersWithinRange(target, 8f))
                {
                    if (member == target) continue;

                    ApplyEffectToObject(DurationType.Temporary, EffectDamageShield(baseAmount, DamageBonus.DAMAGEBONUS_1d8, DamageType.Piercing), member, length);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Bigbys_Forceful_Hand), member);

                    Enmity.ModifyEnmityOnAll(activator, 20);
                }

                StatusEffect.Remove(activator, StatusEffectType.ArcaneSpread);
            }

            CombatPoint.AddCombatPointToAllTagged(activator, SkillType.Arcane, 3);
        }

        private void ArcaneSpikes1()
        {
            _builder.Create(FeatType.ArcaneSpikes1, PerkType.ArcaneSpikes)
                .Name("Arcane Spikes I")
                .HasRecastDelay(RecastGroup.ArcaneSpikes, 10f)
                .IsCastedAbility()
                .HasActivationDelay(4f)
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, _, targetLocation) =>
                {
                    ImpactAction(activator, target, 18);
                });
        }

        private void ArcaneSpikes2()
        {
            _builder.Create(FeatType.ArcaneSpikes2, PerkType.ArcaneSpikes)
                .Name("Arcane Spikes II")
                .HasRecastDelay(RecastGroup.ArcaneSpikes, 10f)
                .IsCastedAbility()
                .HasActivationDelay(4f)
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, _, targetLocation) =>
                {
                    ImpactAction(activator, target, 28);
                });
        }

        private void ArcaneSpikes3()
        {
            _builder.Create(FeatType.ArcaneSpikes3, PerkType.ArcaneSpikes)
                .Name("Arcane Spikes III")
                .HasRecastDelay(RecastGroup.ArcaneSpikes, 10f)
                .IsCastedAbility()
                .HasActivationDelay(4f)
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, _, targetLocation) =>
                {
                    ImpactAction(activator, target, 38);
                });
        }
    }
}
