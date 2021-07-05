using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service.PerkService;

namespace Xenomech.Feature.PerkDefinition
{
    public class PilotingPerkDefinition : IPerkListDefinition
    {
        private readonly PerkBuilder _builder = new PerkBuilder();

        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            MechPiloting();
            MechWeaponry();
            MechDefenses();
            FuelConservation();
            ImprovedFocus();

            return _builder.Build();
        }

        private void MechPiloting()
        {
            _builder.Create(PerkCategoryType.Piloting, PerkType.MechPiloting)
                .Name("Mech Piloting")

                .AddPerkLevel()
                .Description("Grants the ability to pilot tier 1 mechs.")
                .Price(2)
                .GrantsFeat(FeatType.MechPiloting1)

                .AddPerkLevel()
                .Description("Grants the ability to pilot tier 2 mechs.")
                .Price(3)
                .RequirementSkill(SkillType.Piloting, 10)
                .GrantsFeat(FeatType.MechPiloting2)

                .AddPerkLevel()
                .Description("Grants the ability to pilot tier 3 mechs.")
                .Price(4)
                .RequirementSkill(SkillType.Piloting, 20)
                .GrantsFeat(FeatType.MechPiloting3)

                .AddPerkLevel()
                .Description("Grants the ability to pilot tier 4 mechs.")
                .Price(5)
                .RequirementSkill(SkillType.Piloting, 30)
                .GrantsFeat(FeatType.MechPiloting4)

                .AddPerkLevel()
                .Description("Grants the ability to pilot tier 5 mechs.")
                .Price(6)
                .RequirementSkill(SkillType.Piloting, 40)
                .GrantsFeat(FeatType.MechPiloting5);
        }

        private void MechWeaponry()
        {
            _builder.Create(PerkCategoryType.Piloting, PerkType.MechWeaponry)
                .Name("Mech Weaponry")

                .AddPerkLevel()
                .Description("Grants the ability to use tier 1 mech weaponry.")
                .Price(1)
                .GrantsFeat(FeatType.MechWeaponry1)

                .AddPerkLevel()
                .Description("Grants the ability to use tier 2 mech weaponry.")
                .Price(2)
                .RequirementSkill(SkillType.Piloting, 10)
                .GrantsFeat(FeatType.MechWeaponry2)

                .AddPerkLevel()
                .Description("Grants the ability to use tier 3 mech weaponry.")
                .Price(2)
                .RequirementSkill(SkillType.Piloting, 20)
                .GrantsFeat(FeatType.MechWeaponry3)

                .AddPerkLevel()
                .Description("Grants the ability to use tier 4 mech weaponry.")
                .Price(2)
                .RequirementSkill(SkillType.Piloting, 30)
                .GrantsFeat(FeatType.MechWeaponry4)

                .AddPerkLevel()
                .Description("Grants the ability to use tier 5 mech weaponry.")
                .Price(3)
                .RequirementSkill(SkillType.Piloting, 40)
                .GrantsFeat(FeatType.MechWeaponry5);
        }

        private void MechDefenses()
        {
            _builder.Create(PerkCategoryType.Piloting, PerkType.MechDefenses)
                .Name("Mech Defenses")

                .AddPerkLevel()
                .Description("Grants the ability to use tier 1 mech defensive parts.")
                .Price(1)
                .GrantsFeat(FeatType.MechDefenses1)

                .AddPerkLevel()
                .Description("Grants the ability to use tier 2 mech defensive parts.")
                .Price(2)
                .RequirementSkill(SkillType.Piloting, 10)
                .GrantsFeat(FeatType.MechDefenses2)

                .AddPerkLevel()
                .Description("Grants the ability to use tier 3 mech defensive parts.")
                .Price(2)
                .RequirementSkill(SkillType.Piloting, 20)
                .GrantsFeat(FeatType.MechDefenses3)

                .AddPerkLevel()
                .Description("Grants the ability to use tier 4 mech defensive parts.")
                .Price(2)
                .RequirementSkill(SkillType.Piloting, 30)
                .GrantsFeat(FeatType.MechDefenses4)

                .AddPerkLevel()
                .Description("Grants the ability to use tier 5 mech defensive parts.")
                .Price(3)
                .RequirementSkill(SkillType.Piloting, 40)
                .GrantsFeat(FeatType.MechDefenses5);
        }

        private void FuelConservation()
        {
            _builder.Create(PerkCategoryType.Piloting, PerkType.FuelConservation)
                .Name("Fuel Conservation")

                .AddPerkLevel()
                .Description("Your next mech action will use 20% less fuel.")
                .Price(3)
                .RequirementSkill(SkillType.Piloting, 15)
                .GrantsFeat(FeatType.FuelConservation1)

                .AddPerkLevel()
                .Description("Your next mech action will use 40% less fuel.")
                .Price(3)
                .RequirementSkill(SkillType.Piloting, 30)
                .GrantsFeat(FeatType.FuelConservation2);
        }

        private void ImprovedFocus()
        {
            _builder.Create(PerkCategoryType.Piloting, PerkType.ImprovedFocus)
                .Name("Improved Focus")

                .AddPerkLevel()
                .Description("The effectiveness of Focus is increased by 5%.")
                .Price(4)
                .RequirementSkill(SkillType.Piloting, 25)
                .GrantsFeat(FeatType.ImprovedFocus);
        }
    }
}
