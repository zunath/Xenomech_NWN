using System;
using Xenomech.Core;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Entity;
using Xenomech.Service;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature
{
    public static class PersistentHitPoints
    {
        /// <summary>
        /// When a player leaves the server, save their persistent HP.
        /// </summary>
        [NWNEventHandler("mod_exit")]
        public static void SaveHP()
        {
            var player = GetExitingObject();
            if (!GetIsPC(player) || GetIsDM(player)) return;

            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);
            if (dbPlayer == null) return;
            dbPlayer.HP = GetCurrentHitPoints(player);

            DB.Set(playerId, dbPlayer);
        }

        /// <summary>
        /// When a player enters the server, load their persistent HP.
        /// </summary>
        [NWNEventHandler("mod_enter")]
        public static void LoadHP()
        {
            var player = GetEnteringObject();
            if (!GetIsPC(player) || GetIsDM(player)) return;

            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);
            if (dbPlayer == null) return;
            if (dbPlayer.MaxHP <= 0) return; // Check whether MaxHP is initialized

            int hp = GetCurrentHitPoints(player);
            int damage;
            if (dbPlayer.HP < 0)
            {
                damage = hp + Math.Abs(dbPlayer.HP);
            }
            else
            {
                damage = hp - dbPlayer.HP;
            }

            if (damage != 0)
            {
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage), player);
            }
        }
    }
}
