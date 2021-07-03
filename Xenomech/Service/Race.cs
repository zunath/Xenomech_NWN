using System.Collections.Generic;
using Xenomech.Core;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.Creature;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Service
{
    public static class Race
    {
        private class RacialAppearance
        {
            public int HeadId { get; set; } = 1;
            public int SkinColorId { get; set; } = 2;
            public int HairColorId { get; set; }
            public AppearanceType AppearanceType { get; set; } = AppearanceType.Human;
            public float Scale { get; set; } = 1.0f;

            public int NeckId { get; set; } = 1;
            public int TorsoId { get; set; } = 1;
            public int PelvisId { get; set; } = 1;

            public int RightBicepId { get; set; } = 1;
            public int RightForearmId { get; set; } = 1;
            public int RightHandId { get; set; } = 1;
            public int RightThighId { get; set; } = 1;
            public int RightShinId { get; set; } = 1;
            public int RightFootId { get; set; } = 1;

            public int LeftBicepId { get; set; } = 1;
            public int LeftForearmId { get; set; } = 1;
            public int LeftHandId { get; set; } = 1;
            public int LeftThighId { get; set; } = 1;
            public int LeftShinId { get; set; } = 1;
            public int LeftFootId { get; set; } = 1;
        }

        private static readonly Dictionary<RacialType, RacialAppearance> _defaultRaceAppearancesMale = new Dictionary<RacialType, RacialAppearance>();
        private static readonly Dictionary<RacialType, RacialAppearance> _defaultRaceAppearancesFemale = new Dictionary<RacialType, RacialAppearance>();

        /// <summary>
        /// When the module loads, cache all default race appearances.
        /// </summary>
        [NWNEventHandler("mod_load")]
        public static void LoadRaces()
        {
            // Male appearances
            _defaultRaceAppearancesMale[RacialType.Human] = new RacialAppearance();
        }

        /// <summary>
        /// When a player enters the server, apply the proper scaling to their character.
        /// </summary>
        [NWNEventHandler("mod_enter")]
        public static void ApplyWookieeScaling()
        {
            var player = GetEnteringObject();
            if (!GetIsPC(player) || GetIsDM(player)) return;
            var gender = GetGender(player);
            var racialType = GetRacialType(player);

            // Ensure the race + gender configuration exists.
            if (gender == Gender.Male && !_defaultRaceAppearancesMale.ContainsKey(racialType) ||
                gender != Gender.Male && !_defaultRaceAppearancesFemale.ContainsKey(racialType))
                return;

            var config = gender == Gender.Male
                ? _defaultRaceAppearancesMale[racialType]
                : _defaultRaceAppearancesFemale[racialType];

            SetObjectVisualTransform(player, ObjectVisualTransform.Scale, config.Scale);
        }

        /// <summary>
        /// Sets the default race appearance for the player's racial type.
        /// This should be called exactly one time on player initialization.
        /// </summary>
        /// <param name="player">The player whose appearance will be adjusted.</param>
        public static void SetDefaultRaceAppearance(uint player)
        {
            var gender = GetGender(player);
            var racialType = GetRacialType(player);
            var raceConfig = gender == Gender.Male
                ? _defaultRaceAppearancesMale[racialType]
                : _defaultRaceAppearancesFemale[racialType];

            // Appearance, Skin, and Hair
            SetCreatureAppearanceType(player, raceConfig.AppearanceType);
            SetColor(player, ColorChannel.Skin, raceConfig.SkinColorId);
            SetColor(player, ColorChannel.Hair, raceConfig.HairColorId);

            // Body parts
            SetCreatureBodyPart(CreaturePart.Head, raceConfig.HeadId, player);

            SetCreatureBodyPart(CreaturePart.Neck, raceConfig.NeckId, player);
            SetCreatureBodyPart(CreaturePart.Torso, raceConfig.TorsoId, player);
            SetCreatureBodyPart(CreaturePart.Pelvis, raceConfig.PelvisId, player);

            SetCreatureBodyPart(CreaturePart.RightBicep, raceConfig.RightBicepId, player);
            SetCreatureBodyPart(CreaturePart.RightForearm, raceConfig.RightForearmId, player);
            SetCreatureBodyPart(CreaturePart.RightHand, raceConfig.RightHandId, player);
            SetCreatureBodyPart(CreaturePart.RightThigh, raceConfig.RightThighId, player);
            SetCreatureBodyPart(CreaturePart.RightShin, raceConfig.RightShinId, player);
            SetCreatureBodyPart(CreaturePart.RightFoot, raceConfig.RightFootId, player);

            SetCreatureBodyPart(CreaturePart.LeftBicep, raceConfig.LeftBicepId, player);
            SetCreatureBodyPart(CreaturePart.LeftForearm, raceConfig.LeftForearmId, player);
            SetCreatureBodyPart(CreaturePart.LeftHand, raceConfig.LeftHandId, player);
            SetCreatureBodyPart(CreaturePart.LeftThigh, raceConfig.LeftThighId, player);
            SetCreatureBodyPart(CreaturePart.LeftShin, raceConfig.LeftShinId, player);
            SetCreatureBodyPart(CreaturePart.LeftFoot, raceConfig.LeftFootId, player);
        }
    }
}
