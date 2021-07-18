using System;
using System.Collections.Generic;
using System.Linq;
using Xenomech.Core;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.Item;
using Xenomech.Entity;
using Xenomech.Extension;
using Xenomech.Service.MechService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Service
{
    public static class Mech
    {
        private static readonly Dictionary<MechFrameType, MechFrameAttribute> _frames = new Dictionary<MechFrameType, MechFrameAttribute>();
        private static readonly Dictionary<MechLeftArmType, MechLeftArmAttribute> _leftArms = new Dictionary<MechLeftArmType, MechLeftArmAttribute>();
        private static readonly Dictionary<MechRightArmType, MechRightArmAttribute> _rightArms = new Dictionary<MechRightArmType, MechRightArmAttribute>();
        private static readonly Dictionary<MechLegType, MechLegAttribute> _legs = new Dictionary<MechLegType, MechLegAttribute>();

        private static readonly Dictionary<uint, MechDetail> _npcMechs = new Dictionary<uint, MechDetail>();

        /// <summary>
        /// When the module loads, cache all mech frames, arms, and legs.
        /// </summary>
        [NWNEventHandler("mod_load")]
        public static void CacheData()
        {
            var frames = Enum.GetValues(typeof(MechFrameType)).Cast<MechFrameType>();
            foreach (var frame in frames)
            {
                var detail = frame.GetAttribute<MechFrameType, MechFrameAttribute>();
                _frames[frame] = detail;
            }

            var leftArms = Enum.GetValues(typeof(MechLeftArmType)).Cast<MechLeftArmType>();
            foreach (var leftArm in leftArms)
            {
                var detail = leftArm.GetAttribute<MechLeftArmType, MechLeftArmAttribute>();
                _leftArms[leftArm] = detail;
            }

            var rightArms = Enum.GetValues(typeof(MechRightArmType)).Cast<MechRightArmType>();
            foreach (var rightArm in rightArms)
            {
                var detail = rightArm.GetAttribute<MechRightArmType, MechRightArmAttribute>();
                _rightArms[rightArm] = detail;
            }

            var legs = Enum.GetValues(typeof(MechLegType)).Cast<MechLegType>();
            foreach (var leg in legs)
            {
                var detail = leg.GetAttribute<MechLegType, MechLegAttribute>();
                _legs[leg] = detail;
            }

            Console.WriteLine($"Loaded {_frames.Count} mech frames.");
            Console.WriteLine($"Loaded {_leftArms.Count} mech left arms.");
            Console.WriteLine($"Loaded {_rightArms.Count} mech right arms.");
            Console.WriteLine($"Loaded {_legs.Count} mech legs.");
        }

        /// <summary>
        /// Retrieves the mech details associated with a creature.
        /// </summary>
        /// <param name="creature">The creature to get mech details from.</param>
        /// <returns>A MechDetail object.</returns>
        public static MechDetail GetMechDetail(uint creature)
        {
            var mechDetail = new MechDetail();

            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = GetObjectUUID(creature);
                var dbPlayer = DB.Get<Player>(playerId);
                if (dbPlayer.ActiveMechId == Guid.Empty)
                    return mechDetail;

                var dbMech = dbPlayer.Mechs[dbPlayer.ActiveMechId];
                var frame = _frames[dbMech.FrameType];
                var leftArm = _leftArms[dbMech.LeftArmType];
                var rightArm = _rightArms[dbMech.RightArmType];
                var legs = _legs[dbMech.LegType];

                mechDetail.MaxFrameHP = frame.HP;
                mechDetail.MaxLeftArmHP = leftArm.HP;
                mechDetail.MaxRightArmHP = rightArm.HP;
                mechDetail.MaxLegsHP = legs.HP;

                mechDetail.CurrentFrameHP = dbMech.FrameHP;
                mechDetail.CurrentLeftArmHP = dbMech.LeftArmHP;
                mechDetail.CurrentRightArmHP = dbMech.RightArmHP;
                mechDetail.CurrentLegsHP = dbMech.LegHP;
            }
            else
            {
                if (!_npcMechs.ContainsKey(creature))
                    return mechDetail;

                return _npcMechs[creature];
            }

            return mechDetail;
        }

        /// <summary>
        /// When a creature spawns, register it in the cache if it has mech details.
        /// </summary>
        [NWNEventHandler("crea_spawn")]
        public static void RegisterNPCMech()
        {
            var creature = OBJECT_SELF;
            var skin = GetItemInSlot(InventorySlot.CreatureArmor, creature);
            var isMech = false;

            var detail = new MechDetail();

            for (var ip = GetFirstItemProperty(skin); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(skin))
            {
                var ipType = GetItemPropertyType(ip);

                if (ipType == ItemPropertyType.NPCMechFrame)
                {
                    var frameType = (MechFrameType) GetItemPropertySubType(ip);
                    var frame = _frames[frameType];
                    detail.MaxFrameHP = frame.HP;
                    detail.CurrentFrameHP = frame.HP;

                    detail.MaxFuel = frame.Fuel;
                    detail.CurrentFuel = frame.Fuel;
                    isMech = true;
                }
                else if (ipType == ItemPropertyType.NPCMechLeftArm)
                {
                    var leftArmType = (MechLeftArmType)GetItemPropertySubType(ip);
                    var leftArm = _leftArms[leftArmType];
                    detail.MaxLeftArmHP = leftArm.HP;
                    detail.CurrentLeftArmHP = leftArm.HP;
                    isMech = true;
                }
                else if (ipType == ItemPropertyType.NPCMechRightArm)
                {
                    var rightArmType = (MechRightArmType)GetItemPropertySubType(ip);
                    var rightArm = _rightArms[rightArmType];
                    detail.MaxRightArmHP = rightArm.HP;
                    detail.CurrentRightArmHP = rightArm.HP;
                    isMech = true;
                }
                else if (ipType == ItemPropertyType.NPCMechLegs)
                {
                    var legType = (MechLegType)GetItemPropertySubType(ip);
                    var leg = _legs[legType];
                    detail.MaxLegsHP = leg.HP;
                    detail.CurrentLegsHP = leg.HP;
                    isMech = true;
                }
            }

            if (isMech)
            {
                _npcMechs[creature] = detail;
            }
        }

        /// <summary>
        /// When a creature dies, if it was registered as a mech, remove it.
        /// </summary>
        [NWNEventHandler("crea_death")]
        public static void UnregisterNPCMech()
        {
            var creature = OBJECT_SELF;
            if (!_npcMechs.ContainsKey(creature))
                return;

            _npcMechs.Remove(creature);
        }

        /// <summary>
        /// Retrieves the mech frame detail specified.
        /// </summary>
        /// <param name="frameType">The frame type to retrieve.</param>
        /// <returns>A mech frame detail.</returns>
        public static MechFrameAttribute GetFrameDetail(MechFrameType frameType)
        {
            return _frames[frameType];
        }

        /// <summary>
        /// Retrieves the mech left arm detail specified.
        /// </summary>
        /// <param name="leftArmType">The left arm type to retrieve.</param>
        /// <returns>A mech left arm detail.</returns>
        public static MechLeftArmAttribute GetLeftArmDetail(MechLeftArmType leftArmType)
        {
            return _leftArms[leftArmType];
        }

        /// <summary>
        /// Retrieves the mech right arm detail specified.
        /// </summary>
        /// <param name="rightArmType">The right arm type to retrieve.</param>
        /// <returns>A mech right arm detail.</returns>
        public static MechRightArmAttribute GetRightArmDetail(MechRightArmType rightArmType)
        {
            return _rightArms[rightArmType];
        }

        /// <summary>
        /// Retrieves the mech leg detail specified.
        /// </summary>
        /// <param name="legType">The leg type to retrieve.</param>
        /// <returns>A mech leg detail.</returns>
        public static MechLegAttribute GetLegDetail(MechLegType legType)
        {
            return _legs[legType];
        }
    }
}
