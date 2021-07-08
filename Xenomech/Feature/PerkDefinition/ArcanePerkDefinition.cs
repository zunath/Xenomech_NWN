using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Entity;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.PerkService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.PerkDefinition
{
    public class ArcanePerkDefinition: IPerkListDefinition
    {
        private readonly PerkBuilder _builder = new PerkBuilder();

        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            Sazanami();
            ArcaneSpread();
            Ryokusho();
            Kaseii();
            Senkei();
            ClearMind();
            ArcaneSpikes();
            Convert();

            return _builder.Build();
        }

        private void Sazanami()
        {
            _builder.Create(PerkCategoryType.EtherArcane, PerkType.Sazanami)
                .Name("Sazanami")

                .AddPerkLevel()
                .Description("Restores HP to a single target.")
                .Price(2)
                .GrantsFeat(FeatType.Sazanami1)
                .RequirementCharacterType(CharacterType.Mage)

                .AddPerkLevel()
                .Description("Restores HP to a single target.")
                .Price(3)
                .RequirementSkill(SkillType.Arcane, 15)
                .GrantsFeat(FeatType.Sazanami2)
                .RequirementCharacterType(CharacterType.Mage)

                .AddPerkLevel()
                .Description("Restores HP to a single target.")
                .Price(4)
                .RequirementSkill(SkillType.Arcane, 30)
                .GrantsFeat(FeatType.Sazanami3)
                .RequirementCharacterType(CharacterType.Mage);
        }

        private void ArcaneSpread()
        {
            _builder.Create(PerkCategoryType.EtherArcane, PerkType.ArcaneSpread)
                .Name("Arcane Spread")

                .AddPerkLevel()
                .Description("Your next beneficial Arcane ability will target all party members within range.")
                .Price(5)
                .RequirementSkill(SkillType.Arcane, 20)
                .GrantsFeat(FeatType.ArcaneSpread)
                .RequirementCharacterType(CharacterType.Mage);
        }

        private void Ryokusho()
        {
            _builder.Create(PerkCategoryType.EtherArcane, PerkType.Ryokusho)
                .Name("Ryokusho")

                .AddPerkLevel()
                .Description("Removes physical ailments from a single target.")
                .Price(3)
                .RequirementSkill(SkillType.Arcane, 10)
                .GrantsFeat(FeatType.Ryokusho)
                .RequirementCharacterType(CharacterType.Mage);
        }

        private void Kaseii()
        {
            _builder.Create(PerkCategoryType.EtherArcane, PerkType.Kaseii)
                .Name("Kaseii")

                .AddPerkLevel()
                .Description("Grants regeneration to a single target for 5 minutes.")
                .Price(4)
                .RequirementSkill(SkillType.Arcane, 35)
                .GrantsFeat(FeatType.Kaseii)
                .RequirementCharacterType(CharacterType.Mage);
        }

        private void Senkei()
        {
            _builder.Create(PerkCategoryType.EtherArcane, PerkType.Senkei)
                .Name("Senkei")

                .AddPerkLevel()
                .Description("Grants haste to a single target for 30 seconds.")
                .Price(5)
                .RequirementSkill(SkillType.Arcane, 45)
                .GrantsFeat(FeatType.Senkei)
                .RequirementCharacterType(CharacterType.Mage);
        }

        private void ClearMind()
        {
            _builder.Create(PerkCategoryType.EtherArcane, PerkType.ClearMind)
                .Name("Clear Mind")
                .TriggerPurchase((player, type, level) =>
                {
                    var playerId = GetObjectUUID(player);
                    var dbPlayer = DB.Get<Player>(playerId);

                    Stat.AdjustEPRegen(dbPlayer, 1);
                    DB.Set(playerId, dbPlayer);
                })
                .TriggerRefund((player, type, level) =>
                {
                    var playerId = GetObjectUUID(player);
                    var dbPlayer = DB.Get<Player>(playerId);

                    Stat.AdjustEPRegen(dbPlayer, -1);
                    DB.Set(playerId, dbPlayer);
                })

                .AddPerkLevel()
                .Description("Increases automatic regeneration of EP by 1 per tick.")
                .Price(3)
                .RequirementSkill(SkillType.Arcane, 20)
                .GrantsFeat(FeatType.ClearMind1)
                .RequirementCharacterType(CharacterType.Mage)

                .AddPerkLevel()
                .Description("Increases automatic regeneration of EP by an additional 1 per tick.")
                .Price(5)
                .RequirementSkill(SkillType.Arcane, 40)
                .GrantsFeat(FeatType.ClearMind2)
                .RequirementCharacterType(CharacterType.Mage);
        }

        private void ArcaneSpikes()
        {
            _builder.Create(PerkCategoryType.EtherArcane, PerkType.ArcaneSpikes)
                .Name("Arcane Spikes")

                .AddPerkLevel()
                .Description("Grants a damage shield to a single target for 5 minutes.")
                .Price(3)
                .RequirementSkill(SkillType.Arcane, 5)
                .GrantsFeat(FeatType.ArcaneSpikes1)
                .RequirementCharacterType(CharacterType.Mage)

                .AddPerkLevel()
                .Description("Grants a damage shield to a single target for 5 minutes.")
                .Price(4)
                .RequirementSkill(SkillType.Arcane, 25)
                .GrantsFeat(FeatType.ArcaneSpikes1)
                .RequirementCharacterType(CharacterType.Mage)

                .AddPerkLevel()
                .Description("Grants a damage shield to a single target for 5 minutes.")
                .Price(5)
                .RequirementSkill(SkillType.Arcane, 40)
                .GrantsFeat(FeatType.ArcaneSpikes1)
                .RequirementCharacterType(CharacterType.Mage);
        }

        private void Convert()
        {
            _builder.Create(PerkCategoryType.EtherArcane, PerkType.Convert)
                .Name("Convert")

                .AddPerkLevel()
                .Description("Swaps current HP with EP.")
                .Price(4)
                .RequirementSkill(SkillType.Arcane, 30)
                .GrantsFeat(FeatType.Convert)
                .RequirementCharacterType(CharacterType.Mage);
        }
    }
}
