using System;
using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service.AbilityService;
using Xenomech.Service.FactionService;
using Xenomech.Service.MechService;
using Xenomech.Service.TaxiService;

namespace Xenomech.Entity
{
    public class Player: EntityBase
    {
        public Player()
        {
            Settings = new PlayerSettings();
            BaseStats = new Dictionary<AbilityType, int>
            {
                {AbilityType.Vitality, 0},
                {AbilityType.Might, 0},
                {AbilityType.Diplomacy, 0},
                {AbilityType.Perception, 0},
                {AbilityType.Unused, 0},
                {AbilityType.Spirit, 0}
            };
            UpgradedStats = new Dictionary<AbilityType, int>
            {
                {AbilityType.Vitality, 0},
                {AbilityType.Might, 0},
                {AbilityType.Diplomacy, 0},
                {AbilityType.Perception, 0},
                {AbilityType.Unused, 0},
                {AbilityType.Spirit, 0}
            };

            ShowHelmet = true;
            IsUsingDualPistolMode = false;
            EmoteStyle = EmoteStyle.Regular;
            MovementRate = 1.0f;
            MapPins = new Dictionary<string, List<MapPin>>();
            MapProgressions = new Dictionary<string, string>();
            RoleplayProgress = new RoleplayProgress();
            Skills = new Dictionary<SkillType, PlayerSkill>();
            Perks = new Dictionary<PerkType, int>();
            RecastTimes = new Dictionary<RecastGroup, DateTime>();
            Quests = new Dictionary<string, PlayerQuest>();
            UnlockedPerks = new Dictionary<PerkType, DateTime>();
            UnlockedRecipes = new Dictionary<RecipeType, DateTime>();
            CharacterType = CharacterType.Invalid;
            KeyItems = new Dictionary<KeyItemType, DateTime>();
            Guilds = new Dictionary<GuildType, PlayerGuild>();
            SavedOutfits = new Dictionary<int, string>();
            Factions = new Dictionary<FactionType, PlayerFactionStanding>();
            TaxiDestinations = new Dictionary<int, List<TaxiDestinationType>>();
            AbilityPointsByLevel = new Dictionary<int, int>
            {
                {10, 0},
                {20, 0},
                {30, 0},
                {40, 0},
                {50, 0},
            };

            Mechs = new Dictionary<Guid, PlayerMech>();
            ActiveMechId = Guid.Empty;
        }

        public override string KeyPrefix => "Player";

        public int Version { get; set; }
        public string Name { get; set; }
        public int MaxHP { get; set; }
        public int MaxEP { get; set; }
        public int MaxStamina { get; set; }
        public int HP { get; set; }
        public int EP { get; set; }
        public int Stamina { get; set; }
        public int BAB { get; set; }
        public string LocationAreaResref { get; set; }
        public float LocationX { get; set; }
        public float LocationY { get; set; }
        public float LocationZ { get; set; }
        public float LocationOrientation { get; set; }
        public float RespawnLocationX { get; set; }
        public float RespawnLocationY { get; set; }
        public float RespawnLocationZ { get; set; }
        public float RespawnLocationOrientation { get; set; }
        public string RespawnAreaResref { get; set; }
        public int UnallocatedXP { get; set; }
        public int UnallocatedSP { get; set; }
        public int UnallocatedAP { get; set; }
        public int TotalSPAcquired { get; set; }
        public int TotalAPAcquired { get; set; }
        public int RegenerationTick { get; set; }
        public int HPRegen { get; set; }
        public int EPRegen { get; set; }
        public int STMRegen { get; set; }
        public int XPDebt { get; set; }
        public bool IsDeleted { get; set; }
        public bool ShowHelmet { get; set; }
        public bool IsUsingDualPistolMode { get; set; }
        public DateTime? DatePerkRefundAvailable { get; set; }
        public CharacterType CharacterType { get; set; }
        public EmoteStyle EmoteStyle { get; set; }
        public string SerializedHotBar { get; set; }
        public Guid ActiveShipId { get; set; }
        public Guid SelectedShipId { get; set; }
        public AppearanceType OriginalAppearanceType { get; set; }
        public float MovementRate { get; set; }
        public int AbilityRecastReduction { get; set; }
        public Guid ActiveMechId { get; set; }

        public PlayerSettings Settings { get; set; }
        public Dictionary<AbilityType, int> BaseStats { get; set; }
        public Dictionary<AbilityType, int> UpgradedStats { get; set; }
        public RoleplayProgress RoleplayProgress { get; set; }
        public Dictionary<string, List<MapPin>> MapPins { get; set; }
        public Dictionary<string, string> MapProgressions { get; set; }
        public Dictionary<SkillType, PlayerSkill> Skills { get; set; }
        public Dictionary<PerkType, int> Perks { get; set; }
        public Dictionary<RecastGroup, DateTime> RecastTimes { get; set; }
        public Dictionary<string, PlayerQuest> Quests { get; set; }
        public Dictionary<PerkType, DateTime> UnlockedPerks { get; set; }
        public Dictionary<RecipeType, DateTime> UnlockedRecipes { get; set; }
        public Dictionary<KeyItemType, DateTime> KeyItems{ get; set; }
        public Dictionary<GuildType, PlayerGuild> Guilds { get; set; }
        public Dictionary<int, string> SavedOutfits { get; set; }
        public Dictionary<FactionType, PlayerFactionStanding> Factions { get; set; }
        public Dictionary<int, List<TaxiDestinationType>> TaxiDestinations { get; set; }
        public Dictionary<int, int> AbilityPointsByLevel { get; set; }
        public Dictionary<Guid, PlayerMech> Mechs { get; set; }
    }

    public class MapPin
    {
        public int Id { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public string Note { get; set; }
    }

    public class RoleplayProgress
    {
        public int RPPoints { get; set; }
        public ulong TotalRPExpGained { get; set; }
        public ulong SpamMessageCount { get; set; }
    }

    public class PlayerSkill
    {
        public int Rank { get; set; }
        public int XP { get; set; }
        public bool IsLocked { get; set; }
    }

    public class PlayerQuest
    {
        public int CurrentState { get; set; }
        public int TimesCompleted { get; set; }
        public DateTime? DateLastCompleted { get; set; }

        public Dictionary<NPCGroupType, int> KillProgresses { get; set; } = new Dictionary<NPCGroupType, int>();
        public Dictionary<string, int> ItemProgresses { get; set; } = new Dictionary<string, int>();
    }

    public class PlayerSettings
    {
        public int? BattleThemeId { get; set; }
        public bool DisplayAchievementNotification { get; set; }

        public PlayerSettings()
        {
            DisplayAchievementNotification = true;
        }
    }

    public class PlayerGuild
    {
        public int Rank { get; set; }
        public int Points { get; set; }
    }

    public class PlayerFactionStanding
    {
        public int Standing { get; set; }
        public int Points { get; set; }
    }

    public class PlayerMech
    {
        public string Name { get; set; }
        public MechFrameType FrameType { get; set; }
        public MechLeftArmType LeftArmType { get; set; }
        public MechRightArmType RightArmType { get; set; }
        public MechLegType LegType { get; set; }
        public int FrameHP { get; set; }
        public int LeftArmHP { get; set; }
        public int RightArmHP { get; set; }
        public int LegHP { get; set; }
        public int Fuel { get; set; }
    }
}
