using Xenomech.Core;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Entity;
using Xenomech.Service;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature
{
    public static class NaturalRegeneration
    {
        /// <summary>
        /// On module heartbeat, process a player's HP/EP/STM regeneration.
        /// </summary>
        [NWNEventHandler("interval_pc_6s")]
        public static void ProcessRegeneration()
        {
            var player = OBJECT_SELF;
            if (!GetIsPC(player) || GetIsDM(player)) return;

            var tick = GetLocalInt(player, "NATURAL_REGENERATION_TICK") + 1;
            if (tick >= 5) // 6 seconds * 5 = 30 seconds
            {
                var playerId = GetObjectUUID(player);
                var dbPlayer = DB.Get<Player>(playerId);
                var hpRegen = dbPlayer.HPRegen;
                var epRegen = dbPlayer.EPRegen;
                var stmRegen = dbPlayer.STMRegen;

                // HP Regen - Reduce combat log spam and only heal if player is below max HP.
                if (hpRegen > 0 && 
                    GetCurrentHitPoints(player) < GetMaxHitPoints(player))
                {
                    ApplyEffectToObject(DurationType.Instant, EffectHeal(hpRegen), player);
                }

                if (epRegen > 0)
                {
                    Stat.RestoreEP(player, epRegen, dbPlayer);
                }

                if (stmRegen > 0)
                {
                    Stat.RestoreStamina(player, stmRegen, dbPlayer);
                }

                tick = 0;
            }

            SetLocalInt(player, "NATURAL_REGENERATION_TICK", tick);
        }
    }
}