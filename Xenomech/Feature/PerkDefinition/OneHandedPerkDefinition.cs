﻿using System.Collections.Generic;
using Xenomech.Core.NWNX;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service.PerkService;
using static Xenomech.Core.NWScript.NWScript;
using Item = Xenomech.Service.Item;

namespace Xenomech.Feature.PerkDefinition
{
    public class OneHandedPerkDefinition : IPerkListDefinition
    {
        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            var builder = new PerkBuilder();
            Doublehand(builder);
            DualWield(builder);
            WeaponFinesse(builder);
            WeaponFocusVibroblades(builder);
            ImprovedCriticalVibroblades(builder);
            VibrobladeProficiency(builder);
            VibrobladeMastery(builder);
            HackingBlade(builder);
            RiotBlade(builder);
            WeaponFocusFinesseVibroblades(builder);
            ImprovedCriticalFinesseVibroblades(builder);
            FinesseVibrobladeProficiency(builder);
            FinesseVibrobladeMastery(builder);
            PoisonStab(builder);
            Backstab(builder);

            return builder.Build();
        }

        private void Doublehand(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.OneHandedGeneral, PerkType.Doublehand)
                .Name("Doublehand")
                
                .AddPerkLevel()
                .Description("Increases damage of one-handed weapons to 1.5xSTR when no off-hand item is equipped.")
                .Price(3)
                .RequirementSkill(SkillType.OneHanded, 15)
                .GrantsFeat(FeatType.Doublehand)
                .TriggerEquippedItem((player, item, slot, type, level) =>
                {
                    var rightHand = GetItemInSlot(InventorySlot.RightHand, player);
                    var leftHand = GetItemInSlot(InventorySlot.LeftHand, player);

                    // Item is going to right hand and no item is in left hand.
                    if (slot == InventorySlot.RightHand && !GetIsObjectValid(leftHand))
                    {
                        WeaponPlugin.SetOneHalfStrength(item, true, true);
                    }

                    // Item is going to left hand and an item is already in the right hand.
                    if (slot == InventorySlot.LeftHand && GetIsObjectValid(rightHand))
                    {
                        WeaponPlugin.SetOneHalfStrength(rightHand, false, true);
                    }
                })
                .TriggerUnequippedItem((player, item, slot, type, level) =>
                {
                    var itemType = GetBaseItemType(item);
                    var rightHand = GetItemInSlot(InventorySlot.RightHand, player);
                    var rightType = GetBaseItemType(rightHand);
                    var leftHand = GetItemInSlot(InventorySlot.LeftHand, player);
                    var leftType = GetBaseItemType(leftHand);

                    // Item is being unequipped from right hand and there's a weapon in left hand.
                    if (slot == InventorySlot.RightHand &&
                        GetIsObjectValid(leftHand) &&
                        Item.OneHandedMeleeItemTypes.Contains(leftType))
                    {
                        WeaponPlugin.SetOneHalfStrength(leftHand, true, true);
                    }

                    // Item is being unequipped from left hand and there's a weapon in the right hand.
                    if(slot == InventorySlot.LeftHand &&
                       GetIsObjectValid(rightHand) &&
                       Item.OneHandedMeleeItemTypes.Contains(rightType))
                    {
                        WeaponPlugin.SetOneHalfStrength(rightHand, true, true);
                    }

                    // Always remove the item's one-half bonus
                    if (Item.OneHandedMeleeItemTypes.Contains(itemType))
                    {
                        WeaponPlugin.SetOneHalfStrength(item, false, true);
                    }
                });
        }

        private void DualWield(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.OneHandedGeneral, PerkType.DualWield)
                .Name("Dual Wield")

                .AddPerkLevel()
                .Description("Enables the use of two one-handed weapons at the same time at standard NWN penalties.")
                .Price(4)
                .RequirementSkill(SkillType.OneHanded, 25)
                .GrantsFeat(FeatType.DualWield)

                .AddPerkLevel()
                .Description("Grants Two-weapon Fighting feat which reduces attack penalty from -6/-10 to -4/-8 when fighting with two weapons.")
                .Price(5)
                .RequirementSkill(SkillType.OneHanded, 35)
                .GrantsFeat(FeatType.TwoWeaponFighting)

                .AddPerkLevel()
                .Description("Grants Ambidexterity feat which reduces the attack penalty of your off-hand weapon by 4.")
                .Price(6)
                .RequirementSkill(SkillType.OneHanded, 45)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.Ambidexterity);
        }

        private void WeaponFinesse(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.OneHandedGeneral, PerkType.WeaponFinesse)
                .Name("Weapon Finesse")

                .AddPerkLevel()
                .Description("You make melee attack rolls with your DEX score if it is higher than your STR score.")
                .Price(3)
                .RequirementSkill(SkillType.OneHanded, 10)
                .GrantsFeat(FeatType.WeaponFinesse);
        }

        private void WeaponFocusVibroblades(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.OneHandedLongsword, PerkType.WeaponFocusLongswords)
                .Name("Weapon Focus - Vibroblades")

                .AddPerkLevel()
                .Description("You gain the Weapon Focus feat which grants a +1 attack bonus when equipped with vibroblades.")
                .Price(3)
                .RequirementSkill(SkillType.OneHanded, 5)
                .GrantsFeat(FeatType.WeaponFocusLongswords)

                .AddPerkLevel()
                .Description("You gain the Weapon Specialization feat which grants a +2 damage when equipped with vibroblades.")
                .Price(4)
                .RequirementSkill(SkillType.OneHanded, 15)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.WeaponSpecializationLongswords);
        }

        private void ImprovedCriticalVibroblades(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.OneHandedLongsword, PerkType.ImprovedCriticalLongswords)
                .Name("Improved Critical - Vibroblades")

                .AddPerkLevel()
                .Description("Improves the critical hit chance when using a vibroblade.")
                .Price(3)
                .RequirementSkill(SkillType.OneHanded, 25)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.ImprovedCriticalLongswords);
        }

        private void VibrobladeProficiency(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.OneHandedLongsword, PerkType.LongswordProficiency)
                .Name("Vibroblade Proficiency")

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 1 Vibroblades.")
                .Price(2)
                .GrantsFeat(FeatType.LongswordProficiency1)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 2 Vibroblades.")
                .Price(2)
                .RequirementSkill(SkillType.OneHanded, 10)
                .GrantsFeat(FeatType.LongswordProficiency2)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 3 Vibroblades.")
                .Price(2)
                .RequirementSkill(SkillType.OneHanded, 20)
                .GrantsFeat(FeatType.LongswordProficiency3)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 4 Vibroblades.")
                .Price(2)
                .RequirementSkill(SkillType.OneHanded, 30)
                .GrantsFeat(FeatType.LongswordProficiency4)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 5 Vibroblades.")
                .Price(2)
                .RequirementSkill(SkillType.OneHanded, 40)
                .GrantsFeat(FeatType.LongswordProficiency5);
        }

        private void VibrobladeMastery(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.OneHandedLongsword, PerkType.LongswordMastery)
                .Name("Vibroblade Mastery")
                .TriggerEquippedItem((player, item, slot, type, level) =>
                {
                    if (slot != InventorySlot.RightHand) return;

                    var itemType = GetBaseItemType(item);
                    if (Item.VibrobladeBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) + level;
                        CreaturePlugin.SetBaseAttackBonus(player, bab);
                    }
                })
                .TriggerUnequippedItem((player, item, slot, type, level) =>
                {
                    if (slot != InventorySlot.RightHand) return;

                    var itemType = GetBaseItemType(item);
                    if (Item.VibrobladeBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) - level;
                        CreaturePlugin.SetBaseAttackBonus(player, bab);
                    }

                })
                .TriggerPurchase((player, type, level) =>
                {
                    var item = GetItemInSlot(InventorySlot.RightHand, player);
                    var itemType = GetBaseItemType(item);

                    if (Item.VibrobladeBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) + 1;
                        CreaturePlugin.SetBaseAttackBonus(player, bab);
                    }
                })
                .TriggerRefund((player, type, level) =>
                {
                    var item = GetItemInSlot(InventorySlot.RightHand, player);
                    var itemType = GetBaseItemType(item);

                    if (Item.VibrobladeBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) - level;
                        CreaturePlugin.SetBaseAttackBonus(player, bab);
                    }
                })

                .AddPerkLevel()
                .Description("Grants +1 BAB when equipped with a Vibroblade.")
                .Price(8)
                .RequirementSkill(SkillType.OneHanded, 25)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.LongswordMastery1)

                .AddPerkLevel()
                .Description("Grants +2 BAB when equipped with a Vibroblade.")
                .Price(8)
                .RequirementSkill(SkillType.OneHanded, 40)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.LongswordMastery2)

                .AddPerkLevel()
                .Description("Grants +3 BAB when equipped with a Vibroblade.")
                .Price(8)
                .RequirementSkill(SkillType.OneHanded, 50)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.LongswordMastery3);
        }

        private void HackingBlade(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.OneHandedLongsword, PerkType.HackingBlade)
                .Name("Hacking Blade")

                .AddPerkLevel()
                .Description("Your next attack deals an additional 1d4 damage and has a 50% chance to inflict Bleed for 30 seconds.")
                .Price(3)
                .RequirementSkill(SkillType.OneHanded, 15)
                .GrantsFeat(FeatType.HackingBlade1)

                .AddPerkLevel()
                .Description("Your next attack deals an additional 2d4 damage and has a 75% chance to inflict Bleed for 1 minute.")
                .Price(3)
                .RequirementSkill(SkillType.OneHanded, 30)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.HackingBlade2)

                .AddPerkLevel()
                .Description("Your next attack deals an additional 3d4 damage and has a 100% chance to inflict Bleed for 1 minute.")
                .Price(3)
                .RequirementSkill(SkillType.OneHanded, 45)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.HackingBlade3);
        }

        private void RiotBlade(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.OneHandedLongsword, PerkType.RiotBlade)
                .Name("Riot Blade")

                .AddPerkLevel()
                .Description("Instantly deals 1d8 damage to your target.")
                .Price(2)
                .RequirementSkill(SkillType.OneHanded, 5)
                .GrantsFeat(FeatType.RiotBlade1)

                .AddPerkLevel()
                .Description("Instantly deals 2d6 damage to your target.")
                .Price(3)
                .RequirementSkill(SkillType.OneHanded, 20)
                .GrantsFeat(FeatType.RiotBlade2)

                .AddPerkLevel()
                .Description("Instantly deals 3d6 damage to your target.")
                .Price(3)
                .RequirementSkill(SkillType.OneHanded, 35)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.RiotBlade3);
        }

        private void WeaponFocusFinesseVibroblades(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.OneHandedKnife, PerkType.WeaponFocusKnives)
                .Name("Weapon Focus - Finesse Vibroblades")

                .AddPerkLevel()
                .Description("You gain the Weapon Focus feat which grants a +1 attack bonus when equipped with finesse vibroblades.")
                .Price(3)
                .RequirementSkill(SkillType.OneHanded, 5)
                .GrantsFeat(FeatType.WeaponFocusKnives)

                .AddPerkLevel()
                .Description("You gain the Weapon Specialization feat which grants a +2 damage when equipped with finesse vibroblades.")
                .Price(4)
                .RequirementSkill(SkillType.OneHanded, 15)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.WeaponSpecializationKnives);
        }

        private void ImprovedCriticalFinesseVibroblades(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.OneHandedKnife, PerkType.ImprovedCriticalKnives)
                .Name("Improved Critical - Finesse Vibroblades")

                .AddPerkLevel()
                .Description("Improves the critical hit chance when using a finesse vibroblade.")
                .Price(3)
                .RequirementSkill(SkillType.OneHanded, 25)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.ImprovedCriticalKnives);
        }

        private void FinesseVibrobladeProficiency(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.OneHandedKnife, PerkType.KnifeProficiency)
                .Name("Finesse Vibroblade Proficiency")

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 1 Finesse Vibroblades.")
                .Price(2)
                .GrantsFeat(FeatType.KnifeProficiency1)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 2 Finesse Vibroblades.")
                .Price(2)
                .RequirementSkill(SkillType.OneHanded, 10)
                .GrantsFeat(FeatType.KnifeProficiency2)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 3 Finesse Vibroblades.")
                .Price(2)
                .RequirementSkill(SkillType.OneHanded, 20)
                .GrantsFeat(FeatType.KnifeProficiency3)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 4 Finesse Vibroblades.")
                .Price(2)
                .RequirementSkill(SkillType.OneHanded, 30)
                .GrantsFeat(FeatType.KnifeProficiency4)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 5 Finesse Vibroblades.")
                .Price(2)
                .RequirementSkill(SkillType.OneHanded, 40)
                .GrantsFeat(FeatType.KnifeProficiency5);
        }

        private void FinesseVibrobladeMastery(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.OneHandedKnife, PerkType.KnifeMastery)
                .Name("Finesse Vibroblade Mastery")
                .TriggerEquippedItem((player, item, slot, type, level) =>
                {
                    if (slot != InventorySlot.RightHand) return;

                    var itemType = GetBaseItemType(item);
                    if (Item.FinesseVibrobladeBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) + level;
                        CreaturePlugin.SetBaseAttackBonus(player, bab);
                    }
                })
                .TriggerUnequippedItem((player, item, slot, type, level) =>
                {
                    if (slot != InventorySlot.RightHand) return;

                    var itemType = GetBaseItemType(item);
                    if (Item.FinesseVibrobladeBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) - level;
                        CreaturePlugin.SetBaseAttackBonus(player, bab);
                    }

                })
                .TriggerPurchase((player, type, level) =>
                {
                    var item = GetItemInSlot(InventorySlot.RightHand, player);
                    var itemType = GetBaseItemType(item);

                    if (Item.FinesseVibrobladeBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) + 1;
                        CreaturePlugin.SetBaseAttackBonus(player, bab);
                    }
                })
                .TriggerRefund((player, type, level) =>
                {
                    var item = GetItemInSlot(InventorySlot.RightHand, player);
                    var itemType = GetBaseItemType(item);

                    if (Item.FinesseVibrobladeBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) - level;
                        CreaturePlugin.SetBaseAttackBonus(player, bab);
                    }
                })

                .AddPerkLevel()
                .Description("Grants +1 BAB when equipped with a Finesse Vibroblade.")
                .Price(8)
                .RequirementSkill(SkillType.OneHanded, 25)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.KnifeMastery1)

                .AddPerkLevel()
                .Description("Grants +2 BAB when equipped with a Finesse Vibroblade.")
                .Price(8)
                .RequirementSkill(SkillType.OneHanded, 40)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.KnifeMastery2)

                .AddPerkLevel()
                .Description("Grants +3 BAB when equipped with a Finesse Vibroblade.")
                .Price(8)
                .RequirementSkill(SkillType.OneHanded, 50)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.KnifeMastery3);
        }

        private void PoisonStab(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.OneHandedKnife, PerkType.PoisonStab)
                .Name("Poison Stab")

                .AddPerkLevel()
                .Description("Your next attack deals an additional 1d6 damage and has a 50% chance to inflict Poison for 30 seconds.")
                .Price(3)
                .RequirementSkill(SkillType.OneHanded, 15)
                .GrantsFeat(FeatType.PoisonStab1)

                .AddPerkLevel()
                .Description("Your next attack deals an additional 2d6 damage and has a 75% chance to inflict Poison for 1 minute.")
                .Price(3)
                .RequirementSkill(SkillType.OneHanded, 30)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.PoisonStab2)

                .AddPerkLevel()
                .Description("Your next attack deals an additional 3d6 damage and has a 100% chance to inflict Poison for 1 minute.")
                .Price(3)
                .RequirementSkill(SkillType.OneHanded, 45)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.PoisonStab3);
        }

        private void Backstab(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.OneHandedKnife, PerkType.Backstab)
                .Name("Backstab")

                .AddPerkLevel()
                .Description("Deals 2d8 damage to your target when dealt from behind. Damage is halved if not behind target.")
                .Price(2)
                .RequirementSkill(SkillType.OneHanded, 5)
                .GrantsFeat(FeatType.Backstab1)

                .AddPerkLevel()
                .Description("Deals 3d8 damage to your target when dealt from behind. Damage is halved if not behind target.")
                .Price(3)
                .RequirementSkill(SkillType.OneHanded, 20)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.Backstab2)

                .AddPerkLevel()
                .Description("Deals 4d8 damage to your target when dealt from behind. Damage is halved if not behind target.")
                .Price(3)
                .RequirementSkill(SkillType.OneHanded, 35)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.Backstab3);
        }
    }
}
