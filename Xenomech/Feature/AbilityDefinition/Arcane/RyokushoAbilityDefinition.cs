using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.Arcane
{
    public class RyokushoAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Ryokusho();

            return _builder.Build();
        }

        private void Ryokusho()
        {
            _builder.Create(FeatType.Ryokusho, PerkType.Ryokusho)
                .Name("Ryokusho")
                .HasRecastDelay(RecastGroup.Ryokusho, 2f)
                .IsCastedAbility()
                .HasActivationDelay(1f)
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, _, targetLocation) =>
                {
                    // Target first always
                    RemoveEffects(target);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Holy_Aid), target);

                    // If Arcane Spread is active, target nearby party members.
                    if (StatusEffect.HasStatusEffect(activator, StatusEffectType.ArcaneSpread))
                    {
                        foreach (var member in Party.GetAllPartyMembersWithinRange(target, 8f))
                        {
                            if (member == target) continue;

                            RemoveEffects(member);
                            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Holy_Aid), member);
                        }

                        StatusEffect.Remove(activator, StatusEffectType.ArcaneSpread);
                    }
                });
        }

        private void RemoveEffects(uint target)
        {
            StatusEffect.Remove(target, StatusEffectType.Bleed);
            StatusEffect.Remove(target, StatusEffectType.Burn);
            StatusEffect.Remove(target, StatusEffectType.Poison);
            StatusEffect.Remove(target, StatusEffectType.Shock);
            StatusEffect.Remove(target, StatusEffectType.Tranquilize);
        }
    }
}
