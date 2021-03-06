using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xenomech.Core.NWNX;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Feature.DialogDefinition;
using Xenomech.Service;
using Xenomech.Service.ChatCommandService;
using static Xenomech.Core.NWScript.NWScript;
using Dialog = Xenomech.Service.Dialog;
using HoloCom = Xenomech.Service.HoloCom;
using Player = Xenomech.Entity.Player;

namespace Xenomech.Feature.ChatCommandDefinition
{
    public class CharacterChatCommand: IChatCommandListDefinition
    {
        public Dictionary<string, ChatCommandDetail> BuildChatCommands()
        {
            var builder = new ChatCommandBuilder();

            builder.Create("cdkey")
                .Description("Displays your public CD key.")
                .Permissions(AuthorizationLevel.All)
                .Action((user, target, location, args) =>
                {
                    var cdKey = GetPCPublicCDKey(user);
                    SendMessageToPC(user, "Your public CD Key is: " + cdKey);
                });

            builder.Create("rest")
                .Description("Opens the rest menu.")
                .Permissions(AuthorizationLevel.Player)
                .Action((user, target, location, args) =>
                {
                    Dialog.StartConversation(user, user, nameof(RestMenu));
                });


            builder.Create("save")
                .Description("Manually saves your character. Your character also saves automatically every few minutes.")
                .Permissions(AuthorizationLevel.Player)
                .Action((user, target, location, args) =>
                {
                    ExportSingleCharacter(user);
                    SendMessageToPC(user, "Character saved successfully.");
                });

            builder.Create("skills")
                .Description("Opens the skills menu.")
                .Permissions(AuthorizationLevel.Player)
                .Action((user, target, location, args) =>
                {
                    Dialog.StartConversation(user, user, nameof(ViewSkillsDialog));
                });

            builder.Create("endcall")
                .Description("Ends your current HoloCom call.")
                .Permissions(AuthorizationLevel.Player, AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Action((user, target, location, args) =>
                {
                    HoloCom.SetIsInCall(user, HoloCom.GetCallReceiver(user), false);
                });

            builder.Create("recipe", "recipes")
                .Description("Opens the recipes menu.")
                .Permissions(AuthorizationLevel.Player)
                .Action((user, target, location, args) =>
                {
                    Dialog.StartConversation(user, user, nameof(RecipeDialog));
                });

            builder.Create("perk", "perks")
                .Description("Opens the perks menu.")
                .Permissions(AuthorizationLevel.Player)
                .Action((user, target, location, args) =>
                {
                    Dialog.StartConversation(user, user, nameof(ViewPerksDialog));
                });

            DeleteCommand(builder);
            CustomizeCommand(builder);
            ToggleHelmet(builder);
            ToggleDualPistolMode(builder);
            ToggleEmoteStyle(builder);
            ChangeItemName(builder);
            ChangeItemDescription(builder);
            ChangePlayerDescription(builder);
            ConcentrationAbility(builder);
            
            return builder.Build();
        }
        
        private static void DeleteCommand(ChatCommandBuilder builder)
        {
            builder.Create("delete")
                .Description("Permanently deletes your character.")
                .Permissions(AuthorizationLevel.Player)
                .Validate((user, args) =>
                {
                    if (!GetIsPC(user) || GetIsDM(user))
                        return "You can only delete a player character.";

                    var cdKey = GetPCPublicCDKey(user);
                    var enteredCDKey = args.Length > 0 ? args[0] : string.Empty;

                    if (cdKey != enteredCDKey)
                    {
                        return "Invalid CD key entered. Please enter the command as follows: \"/delete <CD Key>\". You can retrieve your CD key with the /CDKey chat command.";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    var lastSubmission = GetLocalString(user, "DELETE_CHARACTER_LAST_SUBMISSION");
                    var isFirstSubmission = true;

                    // Check for the last submission, if any.
                    if (!string.IsNullOrWhiteSpace(lastSubmission))
                    {
                        // Found one, parse it.
                        var dateTime = DateTime.Parse(lastSubmission);
                        if (DateTime.UtcNow <= dateTime.AddSeconds(30))
                        {
                            // Player submitted a second request within 30 seconds of the last one. 
                            // This is a confirmation they want to delete.
                            isFirstSubmission = false;
                        }
                    }

                    // Player hasn't submitted or time has elapsed
                    if (isFirstSubmission)
                    {
                        SetLocalString(user, "DELETE_CHARACTER_LAST_SUBMISSION", DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
                        FloatingTextStringOnCreature("Please confirm your deletion by entering another \"/delete <CD Key>\" command within 30 seconds.", user, false);
                    }
                    else
                    {
                        var playerID = GetObjectUUID(user);
                        var entity = DB.Get<Player>(playerID);
                        entity.IsDeleted = true;
                        DB.Set(playerID, entity);

                        BootPC(user, "Your character has been deleted.");
                        AdministrationPlugin.DeletePlayerCharacter(user, true);
                    }
                });
        }

        private static void CustomizeCommand(ChatCommandBuilder builder)
        {
            builder.Create("customize", "customise")
                .Description("Customizes your character appearance. Only available for use in the entry area or DM customization area.")
                .Permissions(AuthorizationLevel.Player)
                .Validate((user, args) =>
                {
                    // DMs can use this command anywhere.
                    if (GetIsDM(user)) return string.Empty;

                    // Players can only do this in certain areas.
                    var areaResref = GetResRef(GetArea(user));
                    if (areaResref != "ooc_area" && areaResref != "customize_char")
                    {
                        return "Customization can only occur in the starting area or the DM customization area. You can't use this command here.";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    Dialog.StartConversation(user, user, nameof(CharacterCustomizationDialog));
                });
        }

        private static void ToggleHelmet(ChatCommandBuilder builder)
        {
            builder.Create("togglehelmet")
                .Description("Toggles whether your helmet will be shown when equipped.")
                .Permissions(AuthorizationLevel.Player)
                .Action((user, target, location, args) =>
                {
                    var playerId = GetObjectUUID(user);
                    var dbPlayer = DB.Get<Player>(playerId);
                    dbPlayer.ShowHelmet = !dbPlayer.ShowHelmet;
                    
                    DB.Set(playerId, dbPlayer);

                    FloatingTextStringOnCreature(
                        dbPlayer.ShowHelmet ? "Now showing equipped helmet." : "Now hiding equipped helmet.",
                        user,
                        false);

                    var helmet = GetItemInSlot(InventorySlot.Head, user);
                    if (GetIsObjectValid(helmet))
                    {
                        SetHiddenWhenEquipped(helmet, !dbPlayer.ShowHelmet);
                    }
                });
        }

        private static void ToggleDualPistolMode(ChatCommandBuilder builder)
        {
            builder.Create("toggledualpistolmode")
                .Description("Toggles whether or not your pistol will be dual wielded when equipped.")
                .Permissions(AuthorizationLevel.Player)
                .Action((user, target, location, args) =>
                {
                    DualPistolService.ToggleDualPistolMode(user);
                });
        }

        private static void ToggleEmoteStyle(ChatCommandBuilder builder)
        {
            builder.Create("emotestyle")
                .Description("Toggles your emote style between regular and novel.")
                .Permissions(AuthorizationLevel.Player)
                .Action((user, target, location, args) =>
                {
                    var curStyle = Communication.GetEmoteStyle(user);
                    var newStyle = curStyle == EmoteStyle.Novel ? EmoteStyle.Regular : EmoteStyle.Novel;
                    Communication.SetEmoteStyle(user, newStyle);
                    SendMessageToPC(user, $"Toggled emote style to {newStyle}.");
                });
        }

        private static void ChangeItemName(ChatCommandBuilder builder)
        {
            builder.Create("changeitemname", "itemname")
                .Description("Changes the name of an item in your inventory. Example: /changeitemname New Name")
                .Permissions(AuthorizationLevel.Player, AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .RequiresTarget()
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target) ||
                        GetItemPossessor(target) != user ||
                        GetObjectType(target) != ObjectType.Item)
                    {
                        SendMessageToPC(user, "Only items in your inventory may be targeted with this command.");
                        return;
                    }

                    var sb = new StringBuilder();

                    foreach (var arg in args)
                    {
                        sb.Append(' ').Append(arg);
                    }

                    SetName(target, sb.ToString());
                    SendMessageToPC(user, "New name set!");
                });
        }

        private static void ChangeItemDescription(ChatCommandBuilder builder)
        {
            builder.Create("changeitemdescription", "itemdesc")
                .Description("Changes the description of an item in your inventory. Example: /changeitemdescription New Name")
                .Permissions(AuthorizationLevel.Player, AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .RequiresTarget()
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target) ||
                        GetItemPossessor(target) != user ||
                        GetObjectType(target) != ObjectType.Item)
                    {
                        SendMessageToPC(user, "Only items in your inventory may be targeted with this command.");
                        return;
                    }
                    
                    var sb = new StringBuilder();

                    foreach (var arg in args)
                    {
                        sb.Append(' ').Append(arg);
                    }

                    SetDescription(target, sb.ToString());
                    SendMessageToPC(user, "New description set!");
                });
        }

        private static void ChangePlayerDescription(ChatCommandBuilder builder)
        {
            builder.Create("changeplayerdescription", "mydesc")
                .Description("Changes your character's description. Example: /changedescription My new description.")
                .Permissions(AuthorizationLevel.Player, AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Action((user, target, location, args) =>
                {
                    var sb = new StringBuilder();
                    
                    foreach (var arg in args)
                    {
                        sb.Append(' ').Append(arg);
                    }

                    SetDescription(user, sb.ToString());
                    SendMessageToPC(user, "New description set!");
                });
        }

        private static void ConcentrationAbility(ChatCommandBuilder builder)
        {
            builder.Create("concentration", "conc")
                .Description("Tells you what concentration ability you have active. Follow with 'end' (no quotes) to turn your concentration ability off. Example: /concentration end")
                .Permissions(AuthorizationLevel.Player, AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Action((user, target, location, args) =>
                {
                    var doEnd = args.Length > 0 && args[0].ToLower() == "end";

                    if (doEnd)
                    {
                        Ability.EndConcentrationAbility(user);
                    }
                    else
                    {
                        var activeConcentration = Ability.GetActiveConcentration(user);
                        if (activeConcentration.Feat == FeatType.Invalid)
                        {
                            SendMessageToPC(user, "No concentration ability is currently active.");
                        }
                        else
                        {
                            var ability = Ability.GetAbilityDetail(activeConcentration.Feat);
                            SendMessageToPC(user, $"Currently active concentration ability: {ability.Name}");
                        }
                    }
                });
        }
        
    }
}
