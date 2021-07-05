using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service.PerkService;

namespace Xenomech.Feature.PerkDefinition
{
    public class ElementalPerkDefinition: IPerkListDefinition
    {
        private readonly PerkBuilder _builder = new PerkBuilder();

        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            ThermoCube();
            ElementalSpread();
            AquaIce();
            Clarity();
            AnemoBolt();
            ElementalSeal();

            return _builder.Build();
        }

        private void ThermoCube()
        {
            _builder.Create(PerkCategoryType.EtherElemental, PerkType.ThermoCube)
                .Name("Thermo Cube")

                .AddPerkLevel()
                .Description("Inflicts fire damage to a single target.")
                .Price(2)
                .GrantsFeat(FeatType.ThermoCube1)

                .AddPerkLevel()
                .Description("Inflicts fire damage to a single target.")
                .Price(3)
                .RequirementSkill(SkillType.Elemental, 15)
                .GrantsFeat(FeatType.ThermoCube2)

                .AddPerkLevel()
                .Description("Inflicts fire damage to a single target.")
                .Price(4)
                .RequirementSkill(SkillType.Elemental, 30)
                .GrantsFeat(FeatType.ThermoCube3);
        }

        private void ElementalSpread()
        {
            _builder.Create(PerkCategoryType.EtherElemental, PerkType.ElementalSpread)
                .Name("Elemental Spread")

                .AddPerkLevel()
                .Description("Your next offensive Elemental ability will target all enemies within range.")
                .Price(5)
                .RequirementSkill(SkillType.Elemental, 20)
                .GrantsFeat(FeatType.ElementalSpread);
        }

        private void AquaIce()
        {
            _builder.Create(PerkCategoryType.EtherElemental, PerkType.AquaIce)
                .Name("Aqua Ice")

                .AddPerkLevel()
                .Description("Inflicts ice damage to a single target and slows them for 3 seconds.")
                .Price(3)
                .RequirementSkill(SkillType.Elemental, 10)
                .GrantsFeat(FeatType.AquaIce1)

                .AddPerkLevel()
                .Description("Inflicts ice damage to a single target and slows them for 3 seconds.")
                .Price(4)
                .RequirementSkill(SkillType.Elemental, 35)
                .GrantsFeat(FeatType.AquaIce2)

                .AddPerkLevel()
                .Description("Inflicts ice damage to a single target and slows them for 3 seconds.")
                .Price(5)
                .RequirementSkill(SkillType.Elemental, 45)
                .GrantsFeat(FeatType.AquaIce3);
        }

        private void Clarity()
        {
            _builder.Create(PerkCategoryType.EtherElemental, PerkType.Clarity)
                .Name("Clarity")

                .AddPerkLevel()
                .Description("Increases EP pool by 10 points.")
                .Price(3)
                .RequirementSkill(SkillType.Elemental, 20)
                .GrantsFeat(FeatType.Clarity1)

                .AddPerkLevel()
                .Description("Increases EP pool by an additional 10 points.")
                .Price(5)
                .RequirementSkill(SkillType.Elemental, 40)
                .GrantsFeat(FeatType.Clarity2);
        }

        private void AnemoBolt()
        {
            _builder.Create(PerkCategoryType.EtherElemental, PerkType.AnemoBolt)
                .Name("Anemo Bolt")

                .AddPerkLevel()
                .Description("Inflicts electrical damage to a single target and reduces their AC for 20 seconds.")
                .Price(3)
                .RequirementSkill(SkillType.Elemental, 5)
                .GrantsFeat(FeatType.AnemoBolt1)

                .AddPerkLevel()
                .Description("Inflicts electrical damage to a single target and reduces their AC for 20 seconds.")
                .Price(4)
                .RequirementSkill(SkillType.Elemental, 25)
                .GrantsFeat(FeatType.AnemoBolt2)

                .AddPerkLevel()
                .Description("Inflicts electrical damage to a single target and reduces their AC for 20 seconds.")
                .Price(5)
                .RequirementSkill(SkillType.Elemental, 40)
                .GrantsFeat(FeatType.AnemoBolt3);
        }

        private void ElementalSeal()
        {
            _builder.Create(PerkCategoryType.EtherElemental, PerkType.ElementalSeal)
                .Name("Elemental Seal")

                .AddPerkLevel()
                .Description("Doubles the effectiveness of your next offensive Elemental ability.")
                .Price(4)
                .RequirementSkill(SkillType.Elemental, 30)
                .GrantsFeat(FeatType.ElementalSeal);
        }
    }
}
