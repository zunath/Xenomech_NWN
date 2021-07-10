using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.Arcane
{
    public class KaseiiAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Kaseii();

            return _builder.Build();
        }

        private void Kaseii()
        {
            _builder.Create(FeatType.Kaseii, PerkType.Kaseii)
                .Name("Kaseii")
                .HasRecastDelay(RecastGroup.Kaseii, 4f)
                .IsCastedAbility()
                .HasActivationDelay(2f)
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, _, targetLocation) =>
                {
                    var activatorSpirit = GetAbilityModifier(AbilityType.Spirit, activator);
                    var amount = 1 + activatorSpirit / 3;

                    // Target first always
                    ApplyEffectToObject(DurationType.Temporary, EffectRegenerate(amount, 6f), target);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Head_Holy), target);

                    // If Arcane Spread is active, target nearby party members.
                    if (StatusEffect.HasStatusEffect(activator, StatusEffectType.ArcaneSpread))
                    {
                        foreach (var member in Party.GetAllPartyMembersWithinRange(target, 8f))
                        {
                            if (member == target) continue;

                            ApplyEffectToObject(DurationType.Instant, EffectRegenerate(amount, 6f), member);
                            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Head_Holy), member);
                        }

                        StatusEffect.Remove(activator, StatusEffectType.ArcaneSpread);
                    }
                });
        }
    }
}
