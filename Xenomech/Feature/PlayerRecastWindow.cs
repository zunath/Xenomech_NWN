using System;
using System.Linq;
using Xenomech.Core;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Entity;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature
{
    public static class PlayerRecastWindow
    {
        private const int MaxNumberOfRecastTimers = 10;
        private static Gui.IdReservation _recastIdReservation;

        /// <summary>
        /// When the module loads, reserve Gui Ids for both window types.
        /// </summary>
        [NWNEventHandler("mod_load")]
        public static void ReserveGuiIds()
        {
            _recastIdReservation = Gui.ReserveIds(nameof(PlayerRecastWindow) + "_ABILITIES", MaxNumberOfRecastTimers * 2);
        }

        /// <summary>
        /// Every second, redraw the window for the player. Window drawn depends on the mode the player is currently in (Character or Space).
        /// </summary>
        [NWNEventHandler("interval_pc_1s")]
        public static void DrawGuiElements()
        {
            var player = OBJECT_SELF;
            if (GetIsDM(player)) return;

            DrawCharacterRecastComponent(player);
        }

        /// <summary>
        /// Handles drawing the window for ability recast timers.
        /// Only used when player is in Character mode (i.e not space)
        /// </summary>
        /// <param name="player">The player to draw the component for.</param>
        private static void DrawCharacterRecastComponent(uint player)
        {
            static string BuildTimerText(RecastGroup group, DateTime now, DateTime recastTime)
            {
                var recastName = (Recast.GetRecastGroupName(group) + ":").PadRight(14, ' ');
                var delta = recastTime - now;
                var formatTime = delta.ToString(@"hh\:mm\:ss").PadRight(8, ' ');
                return recastName + formatTime;
            }

            const int WindowX = 4;
            const int WindowY = 8;
            const int WindowWidth = 25;

            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);
            var now = DateTime.UtcNow;

            var numberOfRecasts = 0;
            foreach (var (group, dateTime) in dbPlayer.RecastTimes)
            {
                // Skip over any date times that have expired but haven't been cleaned up yet.
                if(dateTime < now) continue;

                // Max of 10 recasts can be shown in the window.
                if (numberOfRecasts >= MaxNumberOfRecastTimers) break;

                var text = BuildTimerText(group, now, dateTime);
                var centerWindowX = Gui.CenterStringInWindow(text, WindowX, WindowWidth);

                numberOfRecasts++;
                PostString(player, text, centerWindowX+2, WindowY + numberOfRecasts, ScreenAnchor.TopRight, 1.1f, Gui.ColorWhite, Gui.ColorWhite, _recastIdReservation.StartId + numberOfRecasts, Gui.TextName);
            }

            if (numberOfRecasts > 0)
            {
                Gui.DrawWindow(player, _recastIdReservation.StartId + MaxNumberOfRecastTimers, ScreenAnchor.TopRight, WindowX, WindowY, WindowWidth-2, 1 + numberOfRecasts, 1.1f);
            }
        }
        
        [NWNEventHandler("interval_pc_1s")]
        public static void CleanUpExpiredRecastTimers()
        {
            var player = OBJECT_SELF;
            if (GetIsDM(player)) return;

            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);
            var now = DateTime.UtcNow;

            foreach (var (group, dateTime) in dbPlayer.RecastTimes)
            {
                if (dateTime > now) continue;

                dbPlayer.RecastTimes.Remove(group);
            }

            DB.Set(playerId, dbPlayer);
        }
    }
}
