//using Random = SWLOR.Game.Server.Service.Random;

using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.Force
{
    public class MindTrickAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            MindTrick1(builder);
            MindTrick2(builder);

            return builder.Build();
        }

        private static string Validation(uint activator, uint target, int level)
        {
            if (GetRacialType(target) == RacialType.Cyborg || GetRacialType(target) == RacialType.Robot)
            {
                return "Mind trick does not work on this creature.";
            }
            else return string.Empty;
        }

        private static void MindTrick1(AbilityBuilder builder)
        {
            builder.Create(FeatType.MindTrick1, PerkType.MindTrick)
                .Name("Mind Trick I")
                .HasRecastDelay(RecastGroup.MindTrick, 60f)
                .HasActivationDelay(2.0f)
                .RequirementFP(3)
                .HasCustomValidation((activator, target, level) =>
                {
                    return Validation(activator, target, level);
                })
                .IsConcentrationAbility(StatusEffectType.MindTrick1)
                .DisplaysVisualEffectWhenActivating(); 
        }

        private static void MindTrick2(AbilityBuilder builder)
        {
            builder.Create(FeatType.MindTrick2, PerkType.MindTrick)
                .Name("Mind Trick II")
                .HasRecastDelay(RecastGroup.MindTrick, 60f)
                .HasActivationDelay(2.0f)
                .RequirementFP(5)
                .HasCustomValidation((activator, target, level) =>
                {
                    return Validation(activator, target, level);
                })
                .IsConcentrationAbility(StatusEffectType.MindTrick2)
                .DisplaysVisualEffectWhenActivating();
        }
    }
}