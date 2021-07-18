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
        private const float GuiElementLife = 1.5f;

        private static Gui.IdReservation _characterIdReservation;
        private static Gui.IdReservation _targetMechIdReservation;

        [NWNEventHandler("mod_load")]
        public static void ReserveIds()
        {
            // Reserve 20 Ids for the player's status
            _characterIdReservation = Gui.ReserveIds(nameof(PlayerStatusWindow) + "_CHARACTER", 20);

            // Reserve another 40 Ids for target mech's status.
            _targetMechIdReservation = Gui.ReserveIds(nameof(PlayerStatusWindow) + "_TARGETMECH", 40);
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

            // Character Mode
            if (dbPlayer.ActiveMechId == Guid.Empty)
            {
                // Natural Characters
                if (isStandard)
                {
                    DrawNaturalCharacterStatusComponent(player);
                }
                // Force Characters
                else
                {
                    DrawNonNaturalCharacterStatusComponent(player);
                }
            }
            // Mech Mode
            else
            {
                // Draw player's status
                DrawMechStatus(player, player, true, _characterIdReservation.StartId);

                // Draw player's target status
                var target = GetAttackTarget(player);
                if (GetIsObjectValid(target))
                {
                    //DrawMechStatus(player, target, false, _targetMechIdReservation.StartId);
                }
            }
        }

        /// <summary>
        /// Draws the HP, EP, and STM status information on the player's screen.
        /// </summary>
        /// <param name="player">The player to draw the component for.</param>
        private static void DrawNaturalCharacterStatusComponent(uint player)
        {
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId) ?? new Player();

            var currentHP = GetCurrentHitPoints(player);
            var maxHP = GetMaxHitPoints(player);
            var currentSTM = dbPlayer.Stamina;
            var maxSTM = Stat.GetMaxStamina(player, dbPlayer);

            // Safety checks to ensure we don't display negative values.
            if (currentSTM < 0)
                currentSTM = 0;
            if (maxSTM < 0)
                maxSTM = 0;

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

            PostString(player, hpText, centerWindowX + 8, WindowY + 2, Anchor, GuiElementLife, Gui.ColorWhite, Gui.ColorWhite, _characterIdReservation.StartId + 2, Gui.TextName);
            PostString(player, stmText, centerWindowX + 8, WindowY + 1, Anchor, GuiElementLife, Gui.ColorWhite, Gui.ColorWhite, _characterIdReservation.StartId, Gui.TextName);

            // Draw the bars
            PostString(player, hpBar, centerWindowX + 2, WindowY + 2, Anchor, GuiElementLife, Gui.ColorHealthBar, Gui.ColorHealthBar, _characterIdReservation.StartId + 3, Gui.GuiFontName);
            PostString(player, stmBar, centerWindowX + 2, WindowY + 1, Anchor, GuiElementLife, Gui.ColorStaminaBar, Gui.ColorStaminaBar, _characterIdReservation.StartId + 5, Gui.GuiFontName);

            // Draw the backgrounds
            PostString(player, backgroundBar, centerWindowX + 2, WindowY + 2, Anchor, GuiElementLife, Gui.ColorBlack, Gui.ColorBlack, _characterIdReservation.StartId + 6, Gui.GuiFontName);
            PostString(player, backgroundBar, centerWindowX + 2, WindowY + 1, Anchor, GuiElementLife, Gui.ColorBlack, Gui.ColorBlack, _characterIdReservation.StartId + 8, Gui.GuiFontName);

            Gui.DrawWindow(player, _characterIdReservation.StartId + 9, Anchor, WindowX, WindowY, WindowWidth - 2, 2, GuiElementLife);
        }

        /// <summary>
        /// Draws the HP, EP, and STM status information on a force player's screen.
        /// </summary>
        /// <param name="player">The player to draw the component for.</param>
        private static void DrawNonNaturalCharacterStatusComponent(uint player)
        {
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId) ?? new Player();

            var currentHP = GetCurrentHitPoints(player);
            var maxHP = GetMaxHitPoints(player);
            var currentEP = dbPlayer.EP;
            var maxEP = Stat.GetMaxEP(player, dbPlayer);
            var currentSTM = dbPlayer.Stamina;
            var maxSTM = Stat.GetMaxStamina(player, dbPlayer);

            // Safety checks to ensure we don't show negative values.
            if (currentEP < 0)
                currentEP = 0;
            if (maxEP < 0)
                maxEP = 0;
            if (currentSTM < 0)
                currentSTM = 0;
            if (maxSTM < 0)
                maxSTM = 0;

            var backgroundBar = BuildBar(1, 1, 22);
            var hpBar = BuildBar(currentHP, maxHP, 22);
            var epBar = BuildBar(currentEP, maxEP, 22);
            var stmBar = BuildBar(currentSTM, maxSTM, 22);

            const int WindowX = 1;
            const int WindowY = 5;
            const int WindowWidth = 25;
            const ScreenAnchor Anchor = ScreenAnchor.BottomRight;

            // Draw order is backwards. The top-most layer needs to be drawn first.
            var centerWindowX = Gui.CenterStringInWindow(backgroundBar, WindowX, WindowWidth);

            // Draw the text
            var hpText = "HP:".PadRight(5, ' ') + $"{currentHP.ToString().PadLeft(4, ' ')} / {maxHP.ToString().PadLeft(4, ' ')}";
            var epText = "EP:".PadRight(5, ' ') + $"{currentEP.ToString().PadLeft(4, ' ')} / {maxEP.ToString().PadLeft(4, ' ')}";
            var stmText = "STM:".PadRight(5, ' ') + $"{currentSTM.ToString().PadLeft(4, ' ')} / {maxSTM.ToString().PadLeft(4, ' ')}";

            PostString(player, hpText, centerWindowX + 8, WindowY + 3, Anchor, GuiElementLife, Gui.ColorWhite, Gui.ColorWhite, _characterIdReservation.StartId + 2, Gui.TextName);
            PostString(player, epText, centerWindowX + 8, WindowY + 2, Anchor, GuiElementLife, Gui.ColorWhite, Gui.ColorWhite, _characterIdReservation.StartId + 1, Gui.TextName);
            PostString(player, stmText, centerWindowX + 8, WindowY + 1, Anchor, GuiElementLife, Gui.ColorWhite, Gui.ColorWhite, _characterIdReservation.StartId, Gui.TextName);

            // Draw the bars
            PostString(player, hpBar, centerWindowX + 2, WindowY + 3, Anchor, GuiElementLife, Gui.ColorHealthBar, Gui.ColorHealthBar, _characterIdReservation.StartId + 3, Gui.GuiFontName);
            PostString(player, epBar, centerWindowX + 2, WindowY + 2, Anchor, GuiElementLife, Gui.ColorEPBar, Gui.ColorEPBar, _characterIdReservation.StartId + 4, Gui.GuiFontName);
            PostString(player, stmBar, centerWindowX + 2, WindowY + 1, Anchor, GuiElementLife, Gui.ColorStaminaBar, Gui.ColorStaminaBar, _characterIdReservation.StartId + 5, Gui.GuiFontName);

            // Draw the backgrounds
            PostString(player, backgroundBar, centerWindowX + 2, WindowY + 3, Anchor, GuiElementLife, Gui.ColorBlack, Gui.ColorBlack, _characterIdReservation.StartId + 6, Gui.GuiFontName);
            PostString(player, backgroundBar, centerWindowX + 2, WindowY + 2, Anchor, GuiElementLife, Gui.ColorBlack, Gui.ColorBlack, _characterIdReservation.StartId + 7, Gui.GuiFontName);
            PostString(player, backgroundBar, centerWindowX + 2, WindowY + 1, Anchor, GuiElementLife, Gui.ColorBlack, Gui.ColorBlack, _characterIdReservation.StartId + 8, Gui.GuiFontName);

            Gui.DrawWindow(player, _characterIdReservation.StartId + 9, Anchor, WindowX, WindowY, WindowWidth - 2, 3, GuiElementLife);
        }

        private static void DrawMechStatus(uint player, uint source, bool showFuel, int reservationIdStart)
        {
            var sourceMech = Mech.GetMechDetail(source);
            var frameHP = sourceMech.CurrentFrameHP;
            var leftArmHP = sourceMech.CurrentLeftArmHP;
            var rightArmHP = sourceMech.CurrentRightArmHP;
            var legsHP = sourceMech.CurrentLegsHP;
            var fuel = sourceMech.CurrentFuel;

            // Safety checks to ensure we don't show negative values.
            if (frameHP < 0)
                frameHP = 0;
            if (leftArmHP < 0)
                leftArmHP = 0;
            if (rightArmHP < 0)
                rightArmHP = 0;
            if (legsHP < 0)
                legsHP = 0;
            if (fuel < 0)
                fuel = 0;

            var backgroundBar = BuildBar(1, 1, 22);
            var frameHPBar = BuildBar(frameHP, sourceMech.MaxFrameHP, 22);
            var leftArmHPBar = BuildBar(leftArmHP, sourceMech.MaxLeftArmHP, 10);
            var rightArmHPBar = BuildBar(rightArmHP, sourceMech.MaxRightArmHP, 10);
            var legsHPBar = BuildBar(legsHP, sourceMech.MaxLegsHP, 22);
            var fuelBar = BuildBar(fuel, sourceMech.MaxFuel, 22);

            const int WindowX = 1;
            const int WindowY = 6;
            const int WindowWidth = 26;
            const ScreenAnchor Anchor = ScreenAnchor.BottomRight;

            // Draw order is backwards. The top-most layer needs to be drawn first.
            var centerWindowX = Gui.CenterStringInWindow(backgroundBar, WindowX, WindowWidth);

            // Draw the text
            var frameHPText = "FR:".PadRight(5, ' ') + $"{frameHP.ToString().PadLeft(4, ' ')} / {sourceMech.MaxFrameHP.ToString().PadLeft(4, ' ')}";
            var leftArmHPText = "LA:".PadRight(3, ' ') + $"{leftArmHP.ToString().PadLeft(3, ' ')}/{sourceMech.MaxLeftArmHP.ToString().PadLeft(3, ' ')}";
            var rightArmHPText = "RA:".PadRight(3, ' ') + $"{rightArmHP.ToString().PadLeft(3, ' ')}/{sourceMech.MaxRightArmHP.ToString().PadLeft(3, ' ')}";
            var legsHPText = "LG:".PadRight(5, ' ') + $"{legsHP.ToString().PadLeft(4, ' ')} / {sourceMech.MaxLegsHP.ToString().PadLeft(4, ' ')}";
            var fuelText = "FL:".PadRight(5, ' ') + $"{fuel.ToString().PadLeft(4, ' ')} / {sourceMech.MaxFuel.ToString().PadLeft(4, ' ')}";

            // First line: Frame HP (Text)
            PostString(player, frameHPText, centerWindowX + 8, WindowY + 4, Anchor, GuiElementLife, Gui.ColorWhite, Gui.ColorWhite, reservationIdStart, Gui.TextName);
            PostString(player, frameHPBar, centerWindowX + 2, WindowY + 4, Anchor, GuiElementLife, Gui.ColorHealthBar, Gui.ColorHealthBar, reservationIdStart + 1, Gui.GuiFontName);

            // Second line: Left Arm / Right Arm HP (Text)
            PostString(player, leftArmHPText, centerWindowX + 14, WindowY + 3, Anchor, GuiElementLife, Gui.ColorWhite, Gui.ColorWhite, reservationIdStart + 2, Gui.TextName);
            PostString(player, leftArmHPBar, centerWindowX + 14, WindowY + 3, Anchor, GuiElementLife, Gui.ColorStaminaBar, Gui.ColorStaminaBar, reservationIdStart + 3, Gui.GuiFontName);
            PostString(player, rightArmHPText, centerWindowX + 4, WindowY + 3, Anchor, GuiElementLife, Gui.ColorWhite, Gui.ColorWhite, reservationIdStart + 4, Gui.TextName);
            PostString(player, rightArmHPBar, centerWindowX + 4, WindowY + 3, Anchor, GuiElementLife, Gui.ColorStaminaBar, Gui.ColorStaminaBar, reservationIdStart + 5, Gui.GuiFontName);

            // Third line: Leg HP (Text)
            PostString(player, legsHPText, centerWindowX + 8, WindowY + 2, Anchor, GuiElementLife, Gui.ColorWhite, Gui.ColorWhite, reservationIdStart + 6, Gui.TextName);
            PostString(player, legsHPBar, centerWindowX + 2, WindowY + 2, Anchor, GuiElementLife, Gui.ColorEPBar, Gui.ColorEPBar, reservationIdStart + 7, Gui.GuiFontName);

            // Fourth line: Fuel (Text)
            if (showFuel)
            {
                PostString(player, fuelText, centerWindowX + 8, WindowY + 1, Anchor, GuiElementLife, Gui.ColorWhite, Gui.ColorWhite, reservationIdStart + 8, Gui.TextName);
                PostString(player, fuelBar, centerWindowX + 2, WindowY + 1, Anchor, GuiElementLife, Gui.ColorCapacitorBar, Gui.ColorCapacitorBar, reservationIdStart + 9, Gui.GuiFontName);
            }
            
            // Draw the backgrounds
            PostString(player, backgroundBar, centerWindowX + 2, WindowY + 3, Anchor, GuiElementLife, Gui.ColorBlack, Gui.ColorBlack, reservationIdStart + 10, Gui.GuiFontName);
            PostString(player, backgroundBar, centerWindowX + 2, WindowY + 2, Anchor, GuiElementLife, Gui.ColorBlack, Gui.ColorBlack, reservationIdStart + 11, Gui.GuiFontName);
            PostString(player, backgroundBar, centerWindowX + 2, WindowY + 1, Anchor, GuiElementLife, Gui.ColorBlack, Gui.ColorBlack, reservationIdStart + 12, Gui.GuiFontName);

            if (showFuel)
            {
                Gui.DrawWindow(player, reservationIdStart + 13, Anchor, WindowX, WindowY, WindowWidth - 2, 4, GuiElementLife);
            }
            else
            {
                Gui.DrawWindow(player, reservationIdStart + 14, Anchor, WindowX, WindowY, WindowWidth - 2, 3, GuiElementLife);
            }
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
