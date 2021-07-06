﻿using System;
using Xenomech.Core.NWNX;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Entity;
using Xenomech.Service;
using Xenomech.Service.DialogService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.DialogDefinition
{
    public class DistributeAbilityPointsDialog: DialogBase
    {
        private const int MaxUpgrades = 10;

        private class Model
        {
            public AbilityType SelectedAbility { get; set; }
            public bool IsConfirming { get; set; }
        }

        private const string MainPageId = "MAIN_PAGE";
        private const string ConfirmUpgradePageId = "CONFIRM_UPGRADE_PAGE";

        public override PlayerDialog SetUp(uint player)
        {
            var builder = new DialogBuilder()
                .WithDataModel(new Model())
                .AddPage(MainPageId, MainPageInit)
                .AddPage(ConfirmUpgradePageId, ConfirmUpgradePageInit)
                .AddBackAction((previousPage, nextPage) =>
                {
                    var model = GetDataModel<Model>();
                    model.IsConfirming = false;
                });


            return builder.Build();
        }

        private void MainPageInit(DialogPage page)
        {
            var player = GetPC();
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);
            var model = GetDataModel<Model>();

            page.Header = ColorToken.Green("Attribute Point Distribution") + "\n" +
                          ColorToken.Green("Unallocated AP: ") + dbPlayer.UnallocatedAP + "\n\n" +
                          "You may distribute your attribute points into the stats of your choosing here.";

            page.AddResponse($"Might [{dbPlayer.UpgradedStats[AbilityType.Might]}/{MaxUpgrades}]", () =>
            {
                model.SelectedAbility = AbilityType.Might;
                ChangePage(ConfirmUpgradePageId);
            });

            page.AddResponse($"Perception [{dbPlayer.UpgradedStats[AbilityType.Perception]}/{MaxUpgrades}]", () =>
            {
                model.SelectedAbility = AbilityType.Perception;
                ChangePage(ConfirmUpgradePageId);
            });

            page.AddResponse($"Vitality [{dbPlayer.UpgradedStats[AbilityType.Vitality]}/{MaxUpgrades}]", () =>
            {
                model.SelectedAbility = AbilityType.Vitality;
                ChangePage(ConfirmUpgradePageId);
            });

            page.AddResponse($"Spirit [{dbPlayer.UpgradedStats[AbilityType.Spirit]}/{MaxUpgrades}]", () =>
            {
                model.SelectedAbility = AbilityType.Spirit;
                ChangePage(ConfirmUpgradePageId);
            });

            page.AddResponse($"Diplomacy [{dbPlayer.UpgradedStats[AbilityType.Diplomacy]}/{MaxUpgrades}]", () =>
            {
                model.SelectedAbility = AbilityType.Diplomacy;
                ChangePage(ConfirmUpgradePageId);
            });
        }

        private void ConfirmUpgradePageInit(DialogPage page)
        {
            var player = GetPC();
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);
            var model = GetDataModel<Model>();
            var attributeName = GetAbilityName(model.SelectedAbility);

            page.Header = ColorToken.Green("Attribute Point Distribution") + "\n" +
                          ColorToken.Green("Upgrades: ") + dbPlayer.UpgradedStats[model.SelectedAbility] + "/" + MaxUpgrades + "\n" +
                          ColorToken.Green("Unallocated AP: ") + dbPlayer.UnallocatedAP + "\n\n" +
                          ColorToken.Red("WARNING: ") + $"You are about to spend 1 AP to increase your {attributeName} attribute.";
            
            if (dbPlayer.UpgradedStats[model.SelectedAbility] >= MaxUpgrades ||
                dbPlayer.UnallocatedAP <= 0)
                return;

            if (model.IsConfirming)
            {
                page.AddResponse(ColorToken.Green("CONFIRM PURCHASE UPGRADE"), () =>
                {
                    model.IsConfirming = false;

                    // Safety validation checks to be sure player still meets requirements.
                    if (dbPlayer.UnallocatedAP <= 0)
                    {
                        SendMessageToPC(player, "You do not have enough AP to purchase this upgrade.");
                        return;
                    }

                    if (dbPlayer.UpgradedStats[model.SelectedAbility] >= MaxUpgrades)
                    {
                        SendMessageToPC(player, "You cannot upgrade this attribute any futher.");
                        return;
                    }

                    dbPlayer.UnallocatedAP--;
                    dbPlayer.UpgradedStats[model.SelectedAbility]++;
                    CreaturePlugin.ModifyRawAbilityScore(player, model.SelectedAbility, 1);

                    DB.Set(playerId, dbPlayer);

                    SendMessageToPC(player, $"Your {attributeName} attribute has increased!");
                });
            }
            else
            {
                page.AddResponse(ColorToken.Green("Purchase Upgrade"), () =>
                {
                    model.IsConfirming = true;
                });
            }
        }

        private string GetAbilityName(AbilityType abilityType)
        {
            switch (abilityType)
            {
                case AbilityType.Might:
                    return "Might";
                case AbilityType.Perception:
                    return "Perception";
                case AbilityType.Vitality:
                    return "Vitality";
                case AbilityType.Spirit:
                    return "Spirit";
                case AbilityType.Diplomacy:
                    return "Diplomacy";
                default:
                    throw new ArgumentOutOfRangeException(nameof(abilityType), abilityType, null);
            }
        }
    }
}
