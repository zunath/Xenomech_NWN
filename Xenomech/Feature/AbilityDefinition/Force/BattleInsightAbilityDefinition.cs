using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service.AbilityService;

namespace Xenomech.Feature.AbilityDefinition.Force
{
    public class BattleInsightAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            BattleInsight1(builder);
            BattleInsight2(builder);

            return builder.Build();
        }

        private static void BattleInsight1(AbilityBuilder builder)
        {
            builder.Create(FeatType.BattleInsight1, PerkType.BattleInsight)
                .Name("Battle Insight I")
                .HasRecastDelay(RecastGroup.BattleInsight, 60f)
                .HasActivationDelay(2.0f)
                .RequirementFP(3)
                .IsConcentrationAbility(StatusEffectType.BattleInsight1)
                .DisplaysVisualEffectWhenActivating();
        }
        private static void BattleInsight2(AbilityBuilder builder)
        {
            builder.Create(FeatType.BattleInsight2, PerkType.BattleInsight)
                .Name("Battle Insight II")
                .HasRecastDelay(RecastGroup.BattleInsight, 60f)
                .HasActivationDelay(2.0f)
                .RequirementFP(5)
                .IsConcentrationAbility(StatusEffectType.BattleInsight2)
                .DisplaysVisualEffectWhenActivating();
        }
    }
}