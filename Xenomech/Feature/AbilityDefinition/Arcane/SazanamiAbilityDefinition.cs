using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;
using Random = Xenomech.Service.Random;

namespace Xenomech.Feature.AbilityDefinition.Arcane
{
    public class SazanamiAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Sazanami1();
            Sazanami2();
            Sazanami3();

            return _builder.Build();
        }

        private void ImpactAction(uint activator, uint target, int baseAmount)
        {
            var activatorSpirit = GetAbilityModifier(AbilityType.Spirit, activator);
            var maxBonus = activatorSpirit * 10;
            var minBonus = activatorSpirit * 5;

            // Recover target first always.
            var amount = baseAmount + Random.Next(minBonus, maxBonus);
            ApplyEffectToObject(DurationType.Instant, EffectHeal(amount), target);
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Healing_L), target);

            // If Arcane Spread is active, heal nearby party members.
            if (StatusEffect.HasStatusEffect(activator, StatusEffectType.ArcaneSpread))
            {
                foreach (var member in Party.GetAllPartyMembersWithinRange(target, 8f))
                {
                    if (member == target) continue;

                    amount = baseAmount + Random.Next(minBonus, maxBonus);
                    ApplyEffectToObject(DurationType.Instant, EffectHeal(amount), member);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Healing_L), member);
                }

                StatusEffect.Remove(activator, StatusEffectType.ArcaneSpread);
            }
        }

        private void Sazanami1()
        {
            _builder.Create(FeatType.Sazanami1, PerkType.Sazanami)
                .Name("Sazanami I")
                .HasRecastDelay(RecastGroup.Sazanami, 2f)
                .IsCastedAbility()
                .HasActivationDelay(1f)
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, _, targetLocation) =>
                {
                    ImpactAction(activator, target, 40);
                });
        }

        private void Sazanami2()
        {
            _builder.Create(FeatType.Sazanami2, PerkType.Sazanami)
                .Name("Sazanami II")
                .HasRecastDelay(RecastGroup.Sazanami, 2f)
                .IsCastedAbility()
                .HasActivationDelay(1f)
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, _, targetLocation) =>
                {
                    ImpactAction(activator, target, 100);
                });
        }

        private void Sazanami3()
        {
            _builder.Create(FeatType.Sazanami3, PerkType.Sazanami)
                .Name("Sazanami III")
                .HasRecastDelay(RecastGroup.Sazanami, 2f)
                .IsCastedAbility()
                .HasActivationDelay(1f)
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, _, targetLocation) =>
                {
                    ImpactAction(activator, target, 160);
                });
        }

    }
}
