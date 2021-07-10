using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.Spiritbond
{
    public class SpiritAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            SpiritOfChange1();
            SpiritOfChange2();
            SpiritOfChange3();

            SpiritOfFaith1();
            SpiritOfFaith2();
            SpiritOfFaith3();

            SpiritOfRage1();
            SpiritOfRage2();
            SpiritOfRage3();

            return _builder.Build();
        }

        private int CalculateEP(uint player, int baseEP)
        {
            var ep = baseEP;

            if (GetHasFeat(FeatType.SpiritualBonding1, player))
            {
                ep--;
            }

            if (GetHasFeat(FeatType.SpiritualBonding2, player))
            {
                ep--;
            }

            if (ep < 0)
                ep = 0;

            return ep;
        }

        private void SpiritOfChange1()
        {
            _builder.Create(FeatType.SpiritOfChange1, PerkType.SpiritOfChange)
                .Name("Spirit of Change I")
                .HasRecastDelay(RecastGroup.SummonSpirit, 10f)
                .HasActivationDelay(4f)
                .IsConcentrationAbility(StatusEffectType.SpiritOfRage1)
                .RequirementEP(player => CalculateEP(player, 2))
                .UsesAnimation(Animation.LoopingConjure1);
        }

        private void SpiritOfChange2()
        {
            _builder.Create(FeatType.SpiritOfChange2, PerkType.SpiritOfChange)
                .Name("Spirit of Change II")
                .HasRecastDelay(RecastGroup.SummonSpirit, 10f)
                .IsConcentrationAbility(StatusEffectType.SpiritOfRage2)
                .HasActivationDelay(4f)
                .RequirementEP(player => CalculateEP(player, 14))
                .UsesAnimation(Animation.LoopingConjure1);
        }

        private void SpiritOfChange3()
        {
            _builder.Create(FeatType.SpiritOfChange3, PerkType.SpiritOfChange)
                .Name("Spirit of Change III")
                .HasRecastDelay(RecastGroup.SummonSpirit, 10f)
                .IsConcentrationAbility(StatusEffectType.SpiritOfRage3)
                .HasActivationDelay(4f)
                .RequirementEP(player => CalculateEP(player, 7))
                .UsesAnimation(Animation.LoopingConjure1);
        }

        private void SpiritOfRage1()
        {
            _builder.Create(FeatType.SpiritOfRage1, PerkType.SpiritOfRage)
                .Name("Spirit of Rage I")
                .HasRecastDelay(RecastGroup.SummonSpirit, 10f)
                .IsConcentrationAbility(StatusEffectType.SpiritOfRage1)
                .HasActivationDelay(4f)
                .RequirementEP(player => CalculateEP(player, 1))
                .UsesAnimation(Animation.LoopingConjure1);
        }

        private void SpiritOfRage2()
        {
            _builder.Create(FeatType.SpiritOfRage2, PerkType.SpiritOfRage)
                .Name("Spirit of Rage II")
                .HasRecastDelay(RecastGroup.SummonSpirit, 10f)
                .IsConcentrationAbility(StatusEffectType.SpiritOfRage2)
                .HasActivationDelay(4f)
                .RequirementEP(player => CalculateEP(player, 3))
                .UsesAnimation(Animation.LoopingConjure1);
        }

        private void SpiritOfRage3()
        {
            _builder.Create(FeatType.SpiritOfRage3, PerkType.SpiritOfRage)
                .Name("Spirit of Rage III")
                .HasRecastDelay(RecastGroup.SummonSpirit, 10f)
                .IsConcentrationAbility(StatusEffectType.SpiritOfRage3)
                .HasActivationDelay(4f)
                .RequirementEP(player => CalculateEP(player, 6))
                .UsesAnimation(Animation.LoopingConjure1);
        }

        private void SpiritOfFaith1()
        {
            _builder.Create(FeatType.SpiritOfFaith1, PerkType.SpiritOfFaith)
                .Name("Spirit of Faith I")
                .HasRecastDelay(RecastGroup.SummonSpirit, 10f)
                .IsConcentrationAbility(StatusEffectType.SpiritOfRage1)
                .HasActivationDelay(4f)
                .RequirementEP(player => CalculateEP(player, 3))
                .UsesAnimation(Animation.LoopingConjure1);
        }

        private void SpiritOfFaith2()
        {
            _builder.Create(FeatType.SpiritOfFaith2, PerkType.SpiritOfFaith)
                .Name("Spirit of Faith II")
                .HasRecastDelay(RecastGroup.SummonSpirit, 10f)
                .IsConcentrationAbility(StatusEffectType.SpiritOfRage2)
                .HasActivationDelay(4f)
                .RequirementEP(player => CalculateEP(player, 5))
                .UsesAnimation(Animation.LoopingConjure1);
        }

        private void SpiritOfFaith3()
        {
            _builder.Create(FeatType.SpiritOfFaith3, PerkType.SpiritOfFaith)
                .Name("Spirit of Faith III")
                .HasRecastDelay(RecastGroup.SummonSpirit, 10f)
                .IsConcentrationAbility(StatusEffectType.SpiritOfRage3)
                .HasActivationDelay(4f)
                .RequirementEP(player => CalculateEP(player, 7))
                .UsesAnimation(Animation.LoopingConjure1);
        }
    }
}
