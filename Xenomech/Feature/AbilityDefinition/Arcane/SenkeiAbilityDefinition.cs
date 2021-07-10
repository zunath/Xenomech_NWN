using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.Arcane
{
    public class SenkeiAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Senkei();

            return _builder.Build();
        }

        private void Senkei()
        {
            _builder.Create(FeatType.Senkei, PerkType.Senkei)
                .Name("Senkei")
                .HasRecastDelay(RecastGroup.Senkei, 10f)
                .IsCastedAbility()
                .HasActivationDelay(3f)
                .UsesAnimation(Animation.LoopingConjure1)
                .HasImpactAction((activator, target, _, targetLocation) =>
                {
                    var activatorSpirit = GetAbilityModifier(AbilityType.Spirit, activator);
                    var length = 30f + activatorSpirit * 2f;

                    // Target first always
                    ApplyEffectToObject(DurationType.Temporary, EffectHaste(), target, length);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Haste), target);
                    Enmity.ModifyEnmityOnAll(activator, 50);

                    // If Arcane Spread is active, target nearby party members.
                    if (StatusEffect.HasStatusEffect(activator, StatusEffectType.ArcaneSpread))
                    {
                        foreach (var member in Party.GetAllPartyMembersWithinRange(target, 8f))
                        {
                            if (member == target) continue;

                            ApplyEffectToObject(DurationType.Temporary, EffectHaste(), member, length);
                            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Haste), member);

                            Enmity.ModifyEnmityOnAll(activator, 50);
                        }

                        StatusEffect.Remove(activator, StatusEffectType.ArcaneSpread);
                    }

                    CombatPoint.AddCombatPointToAllTagged(activator, SkillType.Arcane, 3);
                });
        }
    }
}
