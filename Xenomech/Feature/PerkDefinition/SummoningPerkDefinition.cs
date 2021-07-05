using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service.PerkService;

namespace Xenomech.Feature.PerkDefinition
{
    public class SummoningPerkDefinition: IPerkListDefinition
    {
        private readonly PerkBuilder _builder = new PerkBuilder();

        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            LightningSpirit();
            EtherFlow();
            EarthSpirit();
            SpiritualBonding();
            HolySpirit();
            EtherConduit();

            return _builder.Build();
        }

        private void LightningSpirit()
        {
            _builder.Create(PerkCategoryType.EtherSummoning, PerkType.LightningSpirit)
                .Name("Lightning Spirit")

                .AddPerkLevel()
                .Description("Summons a Lightning Spirit to do your bidding. The Spirit will use electric based ether abilities.")
                .Price(2)
                .GrantsFeat(FeatType.LightningSpirit1)

                .AddPerkLevel()
                .Description("Summons a Lightning Spirit to do your bidding. The Spirit will use electric based ether abilities.")
                .Price(3)
                .RequirementSkill(SkillType.Summoning, 15)
                .GrantsFeat(FeatType.LightningSpirit2)

                .AddPerkLevel()
                .Description("Summons a Lightning Spirit to do your bidding. The Spirit will use electric based ether abilities.")
                .Price(4)
                .RequirementSkill(SkillType.Summoning, 30)
                .GrantsFeat(FeatType.LightningSpirit3);
        }

        private void EtherFlow()
        {
            _builder.Create(PerkCategoryType.EtherSummoning, PerkType.EtherFlow)
                .Name("Ether Flow")

                .AddPerkLevel()
                .Description("Releases your summoned spirit, initating the spirit's pact ability.")
                .Price(5)
                .RequirementSkill(SkillType.Summoning, 20)
                .GrantsFeat(FeatType.EtherFlow);
        }

        private void EarthSpirit()
        {
            _builder.Create(PerkCategoryType.EtherSummoning, PerkType.EarthSpirit)
                .Name("Earth Spirit")

                .AddPerkLevel()
                .Description("Summons a Earth Spirit to do your bidding. The Spirit will use defensive ether abilities.")
                .Price(3)
                .RequirementSkill(SkillType.Summoning, 10)
                .GrantsFeat(FeatType.LightningSpirit1)

                .AddPerkLevel()
                .Description("Summons a Earth Spirit to do your bidding. The Spirit will use defensive ether abilities.")
                .Price(4)
                .RequirementSkill(SkillType.Summoning, 35)
                .GrantsFeat(FeatType.LightningSpirit2)

                .AddPerkLevel()
                .Description("Summons a Earth Spirit to do your bidding. The Spirit will use defensive ether abilities.")
                .Price(5)
                .RequirementSkill(SkillType.Summoning, 45)
                .GrantsFeat(FeatType.LightningSpirit3);
        }

        private void SpiritualBonding()
        {
            _builder.Create(PerkCategoryType.EtherSummoning, PerkType.SpiritualBonding)
                .Name("Spiritual Bonding")

                .AddPerkLevel()
                .Description("Reduces the EP concentration cost of summons by 1 EP/tick.")
                .Price(3)
                .RequirementSkill(SkillType.Summoning, 20)
                .GrantsFeat(FeatType.SpiritualBonding1)

                .AddPerkLevel()
                .Description("Reduces the EP concentration cost of summons by an additional 1 EP/tick.")
                .Price(5)
                .RequirementSkill(SkillType.Summoning, 40)
                .GrantsFeat(FeatType.SpiritualBonding2);
        }

        private void HolySpirit()
        {
            _builder.Create(PerkCategoryType.EtherSummoning, PerkType.HolySpirit)
                .Name("Holy Spirit")

                .AddPerkLevel()
                .Description("Summons a Holy Spirit to do your bidding. The Spirit will use restorative ether abilities.")
                .Price(3)
                .RequirementSkill(SkillType.Summoning, 5)
                .GrantsFeat(FeatType.HolySpirit1)

                .AddPerkLevel()
                .Description("Summons a Holy Spirit to do your bidding. The Spirit will use restorative ether abilities.")
                .Price(4)
                .RequirementSkill(SkillType.Summoning, 25)
                .GrantsFeat(FeatType.HolySpirit2)

                .AddPerkLevel()
                .Description("Summons a Holy Spirit to do your bidding. The Spirit will use restorative ether abilities.")
                .Price(5)
                .RequirementSkill(SkillType.Summoning, 40)
                .GrantsFeat(FeatType.HolySpirit3);
        }

        private void EtherConduit()
        {
            _builder.Create(PerkCategoryType.EtherSummoning, PerkType.EtherConduit)
                .Name("Ether Conduit")

                .AddPerkLevel()
                .Description("Doubles the effectiveness of your Spirit's next Spirit Pact.")
                .Price(4)
                .RequirementSkill(SkillType.Summoning, 30)
                .GrantsFeat(FeatType.EtherConduit);
        }
    }
}
