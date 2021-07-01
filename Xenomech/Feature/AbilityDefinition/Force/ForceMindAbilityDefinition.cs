﻿//using Random = SWLOR.Game.Server.Service.Random;

using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.Force
{
    public class ForceMindAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            ForceMind1(builder);
            ForceMind2(builder);

            return builder.Build();
        }

        private static void ImpactAction(uint activator, uint target, int level)
        {
            float multiplier = 0;
            switch (level)
            {
                case 1:
                    multiplier = 0.25f;
                    break;
                case 2:
                    multiplier = 0.5f;
                    break;
                default:
                    break;
            }
            // Damage user.
            ApplyEffectToObject(DurationType.Instant, EffectDamage((int)(GetCurrentHitPoints(activator) * multiplier)), activator);

            // Recover FP on target.
            Stat.RestoreFP(activator, (int)(GetCurrentHitPoints(activator) * multiplier));

            // Play VFX
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Head_Odd), target);
            
            Enmity.ModifyEnmityOnAll(activator, 1);
            CombatPoint.AddCombatPointToAllTagged(activator, SkillType.Force, 3);
        }

        private static void ForceMind1(AbilityBuilder builder)
        {
            builder.Create(FeatType.ForceMind1, PerkType.ForceMind)
                .Name("Force Mind I")
                .HasRecastDelay(RecastGroup.ForceMind, 60f * 5f)
                .HasActivationDelay(2.0f)
                .IsCastedAbility()
                .DisplaysVisualEffectWhenActivating()
                .HasImpactAction((activator, target, level) =>
                {
                    ImpactAction(activator, target, level);
                });
        }

        private static void ForceMind2(AbilityBuilder builder)
        {
            builder.Create(FeatType.ForceMind2, PerkType.ForceMind)
                .Name("Force Mind II")
                .HasRecastDelay(RecastGroup.ForceMind, 60f * 5f)
                .HasActivationDelay(2.0f)
                .IsCastedAbility()
                .DisplaysVisualEffectWhenActivating()
                .HasImpactAction((activator, target, level) =>
                {
                    ImpactAction(activator, target, level);
                });
        }
    }
}