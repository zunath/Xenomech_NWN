using System;
using Xenomech.Core;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Entity;
using Xenomech.Enumeration;
using Xenomech.Service;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature
{
    public static class PlayerStatusWindow
    {
        private static Gui.IdReservation _characterIdReservation;

        [NWNEventHandler("mod_load")]
        public static void ReserveIds()
        {
            // Reserve 20 Ids for the player's status
            _characterIdReservation = Gui.ReserveIds(nameof(PlayerStatusWindow) + "_CHARACTER", 20);
        }

        /// <summary>
        /// Every second, draws all GUI elements on the player's screen.
        /// </summary>
        [NWNEventHandler("interval_pc_1s")]
        public static void DrawGuiElements()
        {
            var player = OBJECT_SELF;
            if (GetIsDM(player)) return;

            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);
            var isStandard = dbPlayer.CharacterType == CharacterType.Natural;

            // Standard Characters
            if (isStandard)
            {
                DrawStandardCharacterStatusComponent(player);
            }
            // Force Characters
            else
            {
                DrawForceCharacterStatusComponent(player);
            }
        }

        /// <summary>
        /// Draws the HP, FP, and STM status information on the player's screen.
        /// </summary>
        /// <param name="player">The player to draw the component for.</param>
        private static void DrawStandardCharacterStatusComponent(uint player)
        {
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId) ?? new Player();

            var currentHP = GetCurrentHitPoints(player);
            var maxHP = GetMaxHitPoints(player);
            var currentSTM = dbPlayer.Stamina;
            var maxSTM = Stat.GetMaxStamina(player, dbPlayer);

            var backgroundBar = BuildBar(1, 1, 22);
            var hpBar = BuildBar(currentHP, maxHP, 22);
            var stmBar = BuildBar(currentSTM, maxSTM, 22);

            const int WindowX = 1;
            const int WindowY = 5;
            const int WindowWidth = 25;
            const ScreenAnchor Anchor = ScreenAnchor.BottomRight;

            // Draw order is backwards. The top-most layer needs to be drawn first.
            var centerWindowX = Gui.CenterStringInWindow(backgroundBar, WindowX, WindowWidth);

            // Draw the text
            var hpText = "HP:".PadRight(5, ' ') + $"{currentHP.ToString().PadLeft(4, ' ')} / {maxHP.ToString().PadLeft(4, ' ')}";
            var stmText = "STM:".PadRight(5, ' ') + $"{currentSTM.ToString().PadLeft(4, ' ')} / {maxSTM.ToString().PadLeft(4, ' ')}";

            PostString(player, hpText, centerWindowX + 8, WindowY + 2, Anchor, 0.0f, Gui.ColorWhite, Gui.ColorWhite, _characterIdReservation.StartId + 2, Gui.TextName);
            PostString(player, stmText, centerWindowX + 8, WindowY + 1, Anchor, 0.0f, Gui.ColorWhite, Gui.ColorWhite, _characterIdReservation.StartId, Gui.TextName);

            // Draw the bars
            PostString(player, hpBar, centerWindowX + 2, WindowY + 2, Anchor, 0.0f, Gui.ColorHealthBar, Gui.ColorHealthBar, _characterIdReservation.StartId + 3, Gui.FontName);
            PostString(player, stmBar, centerWindowX + 2, WindowY + 1, Anchor, 0.0f, Gui.ColorStaminaBar, Gui.ColorStaminaBar, _characterIdReservation.StartId + 5, Gui.FontName);

            // Draw the backgrounds
            if (!GetLocalBool(player, "PLAYERSTATUSWINDOW_BACKGROUND_DRAWN"))
            {
                PostString(player, backgroundBar, centerWindowX + 2, WindowY + 2, Anchor, 0.0f, Gui.ColorBlack, Gui.ColorBlack, _characterIdReservation.StartId + 6, Gui.FontName);
                PostString(player, backgroundBar, centerWindowX + 2, WindowY + 1, Anchor, 0.0f, Gui.ColorBlack, Gui.ColorBlack, _characterIdReservation.StartId + 8, Gui.FontName);

                Gui.DrawWindow(player, _characterIdReservation.StartId + 9, Anchor, WindowX, WindowY, WindowWidth - 2, 2);
            }
        }

        /// <summary>
        /// Draws the HP, FP, and STM status information on a force player's screen.
        /// </summary>
        /// <param name="player">The player to draw the component for.</param>
        private static void DrawForceCharacterStatusComponent(uint player)
        {
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId) ?? new Player();

            var currentHP = GetCurrentHitPoints(player);
            var maxHP = GetMaxHitPoints(player);
            var currentFP = dbPlayer.FP;
            var maxFP = Stat.GetMaxFP(player, dbPlayer);
            var currentSTM = dbPlayer.Stamina;
            var maxSTM = Stat.GetMaxStamina(player, dbPlayer);

            var backgroundBar = BuildBar(1, 1, 22);
            var hpBar = BuildBar(currentHP, maxHP, 22);
            var fpBar = BuildBar(currentFP, maxFP, 22);
            var stmBar = BuildBar(currentSTM, maxSTM, 22);

            const int WindowX = 1;
            const int WindowY = 5;
            const int WindowWidth = 25;
            const ScreenAnchor Anchor = ScreenAnchor.BottomRight;

            // Draw order is backwards. The top-most layer needs to be drawn first.
            var centerWindowX = Gui.CenterStringInWindow(backgroundBar, WindowX, WindowWidth);

            // Draw the text
            var hpText = "HP:".PadRight(5, ' ') + $"{currentHP.ToString().PadLeft(4, ' ')} / {maxHP.ToString().PadLeft(4, ' ')}";
            var fpText = "FP:".PadRight(5, ' ') + $"{currentFP.ToString().PadLeft(4, ' ')} / {maxFP.ToString().PadLeft(4, ' ')}";
            var stmText = "STM:".PadRight(5, ' ') + $"{currentSTM.ToString().PadLeft(4, ' ')} / {maxSTM.ToString().PadLeft(4, ' ')}";

            PostString(player, hpText, centerWindowX + 8, WindowY + 3, Anchor, 0.0f, Gui.ColorWhite, Gui.ColorWhite, _characterIdReservation.StartId + 2, Gui.TextName);
            PostString(player, fpText, centerWindowX + 8, WindowY + 2, Anchor, 0.0f, Gui.ColorWhite, Gui.ColorWhite, _characterIdReservation.StartId + 1, Gui.TextName);
            PostString(player, stmText, centerWindowX + 8, WindowY + 1, Anchor, 0.0f, Gui.ColorWhite, Gui.ColorWhite, _characterIdReservation.StartId, Gui.TextName);

            // Draw the bars
            PostString(player, hpBar, centerWindowX + 2, WindowY + 3, Anchor, 0.0f, Gui.ColorHealthBar, Gui.ColorHealthBar, _characterIdReservation.StartId + 3, Gui.FontName);
            PostString(player, fpBar, centerWindowX + 2, WindowY + 2, Anchor, 0.0f, Gui.ColorManaBar, Gui.ColorManaBar, _characterIdReservation.StartId + 4, Gui.FontName);
            PostString(player, stmBar, centerWindowX + 2, WindowY + 1, Anchor, 0.0f, Gui.ColorStaminaBar, Gui.ColorStaminaBar, _characterIdReservation.StartId + 5, Gui.FontName);

            // Draw the backgrounds
            if (!GetLocalBool(player, "PLAYERSTATUSWINDOW_BACKGROUND_DRAWN"))
            {
                PostString(player, backgroundBar, centerWindowX + 2, WindowY + 3, Anchor, 0.0f, Gui.ColorBlack, Gui.ColorBlack, _characterIdReservation.StartId + 6, Gui.FontName);
                PostString(player, backgroundBar, centerWindowX + 2, WindowY + 2, Anchor, 0.0f, Gui.ColorBlack, Gui.ColorBlack, _characterIdReservation.StartId + 7, Gui.FontName);
                PostString(player, backgroundBar, centerWindowX + 2, WindowY + 1, Anchor, 0.0f, Gui.ColorBlack, Gui.ColorBlack, _characterIdReservation.StartId + 8, Gui.FontName);
                
                Gui.DrawWindow(player, _characterIdReservation.StartId + 9, Anchor, WindowX, WindowY, WindowWidth - 2, 3);
            }
        }
        
        [NWNEventHandler("mod_exit")]
        public static void RemoveTempVariables()
        {
            var exiting = GetExitingObject();
            DeleteLocalBool(exiting, "PLAYERSTATUSWINDOW_BACKGROUND_DRAWN");
        }

        /// <summary>
        /// Builds a bar for display with the PostString call.
        /// </summary>
        /// <param name="current">The current value to display.</param>
        /// <param name="maximum">The maximum value to display.</param>
        /// <param name="width"></param>
        /// <returns></returns>
        private static string BuildBar(int current, int maximum, int width)
        {
            if (current <= 0) return string.Empty;

            var unitsPerWidth = (maximum / (float)width);
            var currentNumber = Math.Ceiling(current / unitsPerWidth);
            string bar = string.Empty;

            // When the anchor is at the top-right, the drawing is backwards.
            // We still need to add spaces to the end of the bar to ensure it's showing the
            // empty space.
            for (var x = 0; x < width; x++)
            {
                if (x < currentNumber)
                {
                    bar += Gui.BlankWhite;
                }
                else
                {
                    bar += " ";
                }
            }

            return bar;
        }
    }
}
