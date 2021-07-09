using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service.PerkService;

namespace Xenomech.Feature.PerkDefinition
{
    public class SpiritbondPerkDefinition: IPerkListDefinition
    {
        private readonly PerkBuilder _builder = new PerkBuilder();

        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            SpiritOfRage();
            PartingGift();
            SpiritOfChange();
            SpiritualBonding();
            SpiritOfFaith();
            EtherConduit();

            return _builder.Build();
        }

        private void SpiritOfRage()
        {
            _builder.Create(PerkCategoryType.EtherSpiritbond, PerkType.SpiritOfRage)
                .Name("Spirit of Rage")

                .AddPerkLevel()
                .Description("Summons a Rage Spirit to do your bidding. The Spirit will use electric based ether abilities.")
                .Price(2)
                .GrantsFeat(FeatType.SpiritOfRage1)
                .RequirementCharacterType(CharacterType.Mage)

                .AddPerkLevel()
                .Description("Summons a Rage Spirit to do your bidding. The Spirit will use electric based ether abilities.")
                .Price(3)
                .RequirementSkill(SkillType.Spiritbond, 15)
                .RequirementCharacterType(CharacterType.Mage)
                .GrantsFeat(FeatType.SpiritOfRage2)

                .AddPerkLevel()
                .Description("Summons a Rage Spirit to do your bidding. The Spirit will use electric based ether abilities.")
                .Price(4)
                .RequirementSkill(SkillType.Spiritbond, 30)
                .RequirementCharacterType(CharacterType.Mage)
                .GrantsFeat(FeatType.SpiritOfRage3);
        }

        private void PartingGift()
        {
            _builder.Create(PerkCategoryType.EtherSpiritbond, PerkType.PartingGift)
                .Name("Parting Gift")

                .AddPerkLevel()
                .Description("Releases your summoned spirit, initating the spirit's pact ability.")
                .Price(5)
                .RequirementSkill(SkillType.Spiritbond, 20)
                .RequirementCharacterType(CharacterType.Mage)
                .GrantsFeat(FeatType.PartingGift);
        }

        private void SpiritOfChange()
        {
            _builder.Create(PerkCategoryType.EtherSpiritbond, PerkType.SpiritOfChange)
                .Name("Spirit of Change")

                .AddPerkLevel()
                .Description("Summons a Change Spirit to do your bidding. The Spirit will use defensive ether abilities.")
                .Price(3)
                .RequirementSkill(SkillType.Spiritbond, 10)
                .RequirementCharacterType(CharacterType.Mage)
                .GrantsFeat(FeatType.SpiritOfChange1)

                .AddPerkLevel()
                .Description("Summons a Change Spirit to do your bidding. The Spirit will use defensive ether abilities.")
                .Price(4)
                .RequirementSkill(SkillType.Spiritbond, 35)
                .RequirementCharacterType(CharacterType.Mage)
                .GrantsFeat(FeatType.SpiritOfChange2)

                .AddPerkLevel()
                .Description("Summons a Change Spirit to do your bidding. The Spirit will use defensive ether abilities.")
                .Price(5)
                .RequirementSkill(SkillType.Spiritbond, 45)
                .RequirementCharacterType(CharacterType.Mage)
                .GrantsFeat(FeatType.SpiritOfChange3);
        }

        private void SpiritualBonding()
        {
            _builder.Create(PerkCategoryType.EtherSpiritbond, PerkType.SpiritualBonding)
                .Name("Spiritual Bonding")

                .AddPerkLevel()
                .Description("Reduces the EP concentration cost of summons by 1 EP/tick.")
                .Price(3)
                .RequirementSkill(SkillType.Spiritbond, 20)
                .RequirementCharacterType(CharacterType.Mage)
                .GrantsFeat(FeatType.SpiritualBonding1)

                .AddPerkLevel()
                .Description("Reduces the EP concentration cost of summons by an additional 1 EP/tick.")
                .Price(5)
                .RequirementSkill(SkillType.Spiritbond, 40)
                .RequirementCharacterType(CharacterType.Mage)
                .GrantsFeat(FeatType.SpiritualBonding2);
        }

        private void SpiritOfFaith()
        {
            _builder.Create(PerkCategoryType.EtherSpiritbond, PerkType.SpiritOfFaith)
                .Name("Spirit of Faith")

                .AddPerkLevel()
                .Description("Summons a Faith Spirit to do your bidding. The Spirit will use restorative ether abilities.")
                .Price(3)
                .RequirementSkill(SkillType.Spiritbond, 5)
                .RequirementCharacterType(CharacterType.Mage)
                .GrantsFeat(FeatType.SpiritOfFaith1)

                .AddPerkLevel()
                .Description("Summons a Faith Spirit to do your bidding. The Spirit will use restorative ether abilities.")
                .Price(4)
                .RequirementSkill(SkillType.Spiritbond, 25)
                .RequirementCharacterType(CharacterType.Mage)
                .GrantsFeat(FeatType.SpiritOfFaith2)

                .AddPerkLevel()
                .Description("Summons a Faith Spirit to do your bidding. The Spirit will use restorative ether abilities.")
                .Price(5)
                .RequirementSkill(SkillType.Spiritbond, 40)
                .RequirementCharacterType(CharacterType.Mage)
                .GrantsFeat(FeatType.SpiritOfFaith3);
        }

        private void EtherConduit()
        {
            _builder.Create(PerkCategoryType.EtherSpiritbond, PerkType.EtherConduit)
                .Name("Ether Conduit")

                .AddPerkLevel()
                .Description("Doubles the effectiveness of your Spirit's next Spirit Pact.")
                .Price(4)
                .RequirementSkill(SkillType.Spiritbond, 30)
                .RequirementCharacterType(CharacterType.Mage)
                .GrantsFeat(FeatType.EtherConduit);
        }
    }
}
