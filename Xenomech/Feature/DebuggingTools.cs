using System;
using Xenomech.Core;
using Xenomech.Entity;
using Xenomech.Service;
using Xenomech.Service.MechService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature
{
    public static class DebuggingTools
    {
        [NWNEventHandler("test")]
        public static void Test()
        {
            var player = GetLastUsedBy();
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);

            var mechId = Guid.NewGuid();
            var frame = Mech.GetFrameDetail(MechFrameType.TestFrame);
            var leftArm = Mech.GetLeftArmDetail(MechLeftArmType.TestLeftArm);
            var rightArm = Mech.GetRightArmDetail(MechRightArmType.TestRightArm);
            var legs = Mech.GetLegDetail(MechLegType.TestLegs);

            dbPlayer.Mechs.Add(mechId, new PlayerMech
            {
                Name = "Test Mech",

                FrameType = MechFrameType.TestFrame,
                LeftArmType = MechLeftArmType.TestLeftArm,
                RightArmType = MechRightArmType.TestRightArm,
                LegType = MechLegType.TestLegs,

                FrameHP = frame.HP,
                LeftArmHP = leftArm.HP,
                RightArmHP = rightArm.HP,
                LegHP = legs.HP,

                Fuel = frame.Fuel
            });

            dbPlayer.ActiveMechId = mechId;

            DB.Set(playerId, dbPlayer);
        }

        [NWNEventHandler("test2")]
        public static void ClearMech()
        {
            var player = GetLastUsedBy();
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);

            dbPlayer.ActiveMechId = Guid.Empty;

            DB.Set(playerId, dbPlayer);
        }
    }
}
