using System.Collections.Generic;
using Xenomech.Core.NWNX;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service.PerkService;
using static Xenomech.Core.NWScript.NWScript;
using Item = Xenomech.Service.Item;

namespace Xenomech.Feature.PerkDefinition
{
    public class TwoHandedPerkDefinition : IPerkListDefinition
    {
        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            var builder = new PerkBuilder();
            PowerAttack(builder);
            SuperiorWeaponFocus(builder);
            IncreasedMultiplier(builder);
            Cleave(builder);
            WeaponFocusHeavyVibroblades(builder);
            ImprovedCriticalHeavyVibroblades(builder);
            HeavyVibrobladeProficiency(builder);
            HeavyVibrobladeMastery(builder);
            CrescentMoon(builder);
            HardSlash(builder);
            WeaponFocusPolearms(builder);
            ImprovedCriticalPolearms(builder);
            PolearmProficiency(builder);
            PolearmMastery(builder);
            Skewer(builder);
            DoubleThrust(builder);
            WeaponFocusTwinBlades(builder);
            ImprovedCriticalTwinBlades(builder);
            TwinBladeProficiency(builder);
            TwinBladeMastery(builder);
            LegSweep(builder);
            CrossCut(builder);

            return builder.Build();
        }

        private void PowerAttack(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedGeneral, PerkType.PowerAttack)
                .Name("Power Attack")

                .AddPerkLevel()
                .Description("Grants the Power Attack feat which grants a +5 bonus to damage roll at the cost of -5 to attack roll.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 15)
                .GrantsFeat(FeatType.PowerAttack)

                .AddPerkLevel()
                .Description("Grants the Improved Power Attack feat which grants a +10 bonus to damage roll at the cost of -10 to attack roll.")
                .Price(4)
                .RequirementSkill(SkillType.TwoHanded, 25)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.ImprovedPowerAttack);
        }

        private void SuperiorWeaponFocus(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedGeneral, PerkType.SuperiorWeaponFocus)
                .Name("Superior Weapon Focus")

                .AddPerkLevel()
                .Description("Two-Handed weapons gain +1 bonus to all attack rolls.")
                .Price(5)
                .RequirementSkill(SkillType.TwoHanded, 35)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.SuperiorWeaponFocus);
        }

        private void IncreasedMultiplier(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedGeneral, PerkType.IncreasedMultiplier)
                .Name("Increased Multiplier")

                .AddPerkLevel()
                .Description("Two-Handed weapons gain x1 to all critical hits.")
                .Price(6)
                .RequirementSkill(SkillType.TwoHanded, 45)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.IncreaseMultiplier);
        }

        private void Cleave(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedGeneral, PerkType.Cleave)
                .Name("Cleave")

                .AddPerkLevel()
                .Description("Grants a free attack against any opponent who is within melee range when an enemy is killed.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 10)
                .GrantsFeat(FeatType.Cleave);
        }

        private void WeaponFocusHeavyVibroblades(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedGreatsword, PerkType.WeaponFocusGreatswords)
                .Name("Weapon Focus - Heavy Vibroblades")

                .AddPerkLevel()
                .Description("You gain the Weapon Focus feat which grants a +1 attack bonus when equipped with heavy vibroblades.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 5)
                .GrantsFeat(FeatType.WeaponFocusGreatswords)

                .AddPerkLevel()
                .Description("You gain the Weapon Specialization feat which grants a +2 damage when equipped with heavy vibroblades.")
                .Price(4)
                .RequirementSkill(SkillType.TwoHanded, 15)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.WeaponSpecializationGreatswords);
        }

        private void ImprovedCriticalHeavyVibroblades(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedGreatsword, PerkType.ImprovedCriticalGreatswords)
                .Name("Improved Critical - Heavy Vibroblades")

                .AddPerkLevel()
                .Description("Improves the critical hit chance when using a heavy vibroblade.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 25)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.ImprovedCriticalGreatswords);
        }

        private void HeavyVibrobladeProficiency(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedGreatsword, PerkType.GreatswordProficiency)
                .Name("Heavy Vibroblade Proficiency")

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 1 Heavy Vibroblades.")
                .Price(2)
                .GrantsFeat(FeatType.GreatswordProficiency1)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 2 Heavy Vibroblades.")
                .Price(2)
                .RequirementSkill(SkillType.TwoHanded, 10)
                .GrantsFeat(FeatType.GreatswordProficiency2)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 3 Heavy Vibroblades.")
                .Price(2)
                .RequirementSkill(SkillType.TwoHanded, 20)
                .GrantsFeat(FeatType.GreatswordProficiency3)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 4 Heavy Vibroblades.")
                .Price(2)
                .RequirementSkill(SkillType.TwoHanded, 30)
                .GrantsFeat(FeatType.GreatswordProficiency4)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 5 Heavy Vibroblades.")
                .Price(2)
                .RequirementSkill(SkillType.TwoHanded, 40)
                .GrantsFeat(FeatType.GreatswordProficiency5);
        }

        private void HeavyVibrobladeMastery(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedGreatsword, PerkType.GreatswordMastery)
                .Name("Heavy Vibroblade Mastery")
                .TriggerEquippedItem((player, item, slot, type, level) =>
                {
                    if (slot != InventorySlot.RightHand) return;

                    var itemType = GetBaseItemType(item);
                    if (Item.HeavyVibrobladeBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) + level;
                        Creature.SetBaseAttackBonus(player, bab);
                    }
                })
                .TriggerUnequippedItem((player, item, slot, type, level) =>
                {
                    if (slot != InventorySlot.RightHand) return;

                    var itemType = GetBaseItemType(item);
                    if (Item.HeavyVibrobladeBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) - level;
                        Creature.SetBaseAttackBonus(player, bab);
                    }

                })
                .TriggerPurchase((player, type, level) =>
                {
                    var item = GetItemInSlot(InventorySlot.RightHand, player);
                    var itemType = GetBaseItemType(item);

                    if (Item.HeavyVibrobladeBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) + 1;
                        Creature.SetBaseAttackBonus(player, bab);
                    }
                })
                .TriggerRefund((player, type, level) =>
                {
                    var item = GetItemInSlot(InventorySlot.RightHand, player);
                    var itemType = GetBaseItemType(item);

                    if (Item.HeavyVibrobladeBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) - level;
                        Creature.SetBaseAttackBonus(player, bab);
                    }
                })

                .AddPerkLevel()
                .Description("Grants +1 BAB when equipped with a Heavy Vibroblade.")
                .Price(8)
                .RequirementSkill(SkillType.TwoHanded, 25)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.GreatswordMastery1)

                .AddPerkLevel()
                .Description("Grants +2 BAB when equipped with a Heavy Vibroblade.")
                .Price(8)
                .RequirementSkill(SkillType.TwoHanded, 40)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.GreatswordMastery2)

                .AddPerkLevel()
                .Description("Grants +3 BAB when equipped with a Heavy Vibroblade.")
                .Price(8)
                .RequirementSkill(SkillType.TwoHanded, 50)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.GreatswordMastery3);
        }

        private void CrescentMoon(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedGreatsword, PerkType.CrescentMoon)
                .Name("Crescent Moon")

                .AddPerkLevel()
                .Description("Your next attack deals an additional 2d4 damage and inflicts stun for 3 seconds.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 15)
                .GrantsFeat(FeatType.CrescentMoon1)

                .AddPerkLevel()
                .Description("Your next attack deals an additional 3d4 damage and inflicts stun for 3 seconds.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 30)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.CrescentMoon2)

                .AddPerkLevel()
                .Description("Your next attack deals an additional 4d4 damage and inflicts stun for 3 seconds.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 45)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.CrescentMoon3);
        }

        private void HardSlash(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedGreatsword, PerkType.HardSlash)
                .Name("Hard Slash")

                .AddPerkLevel()
                .Description("Instantly deals 1d12 damage to your target.")
                .Price(2)
                .RequirementSkill(SkillType.TwoHanded, 5)
                .GrantsFeat(FeatType.HardSlash1)

                .AddPerkLevel()
                .Description("Instantly deals 2d8 damage to your target.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 20)
                .GrantsFeat(FeatType.HardSlash2)

                .AddPerkLevel()
                .Description("Instantly deals 3d8 damage to your target.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 35)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.HardSlash3);
        }

        private void WeaponFocusPolearms(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedPolearm, PerkType.WeaponFocusPolearms)
                .Name("Weapon Focus - Polearms")

                .AddPerkLevel()
                .Description("You gain the Weapon Focus feat which grants a +1 attack bonus when equipped with polearms.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 5)
                .GrantsFeat(FeatType.WeaponFocusPolearms)

                .AddPerkLevel()
                .Description("You gain the Weapon Specialization feat which grants a +2 damage when equipped with polearms.")
                .Price(4)
                .RequirementSkill(SkillType.TwoHanded, 15)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.WeaponSpecializationPolearms);
        }

        private void ImprovedCriticalPolearms(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedPolearm, PerkType.ImprovedCriticalPolearms)
                .Name("Improved Critical - Polearms")

                .AddPerkLevel()
                .Description("Improves the critical hit chance when using a polearm.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 25)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.ImprovedCriticalPolearms);
        }

        private void PolearmProficiency(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedPolearm, PerkType.PolearmProficiency)
                .Name("Polearm Proficiency")

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 1 polearms.")
                .Price(2)
                .GrantsFeat(FeatType.PolearmProficiency1)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 2 polearms.")
                .Price(2)
                .RequirementSkill(SkillType.TwoHanded, 10)
                .GrantsFeat(FeatType.PolearmProficiency2)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 3 polearms.")
                .Price(2)
                .RequirementSkill(SkillType.TwoHanded, 20)
                .GrantsFeat(FeatType.PolearmProficiency3)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 4 polearms.")
                .Price(2)
                .RequirementSkill(SkillType.TwoHanded, 30)
                .GrantsFeat(FeatType.PolearmProficiency4)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 5 polearms.")
                .Price(2)
                .RequirementSkill(SkillType.TwoHanded, 40)
                .GrantsFeat(FeatType.PolearmProficiency5);
        }

        private void PolearmMastery(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedPolearm, PerkType.PolearmMastery)
                .Name("Polearm Mastery")
                .TriggerEquippedItem((player, item, slot, type, level) =>
                {
                    if (slot != InventorySlot.RightHand) return;

                    var itemType = GetBaseItemType(item);
                    if (Item.PolearmBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) + level;
                        Creature.SetBaseAttackBonus(player, bab);
                    }
                })
                .TriggerUnequippedItem((player, item, slot, type, level) =>
                {
                    if (slot != InventorySlot.RightHand) return;

                    var itemType = GetBaseItemType(item);
                    if (Item.PolearmBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) - level;
                        Creature.SetBaseAttackBonus(player, bab);
                    }

                })
                .TriggerPurchase((player, type, level) =>
                {
                    var item = GetItemInSlot(InventorySlot.RightHand, player);
                    var itemType = GetBaseItemType(item);

                    if (Item.PolearmBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) + 1;
                        Creature.SetBaseAttackBonus(player, bab);
                    }
                })
                .TriggerRefund((player, type, level) =>
                {
                    var item = GetItemInSlot(InventorySlot.RightHand, player);
                    var itemType = GetBaseItemType(item);

                    if (Item.PolearmBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) - level;
                        Creature.SetBaseAttackBonus(player, bab);
                    }
                })

                .AddPerkLevel()
                .Description("Grants +1 BAB when equipped with a Polearm.")
                .Price(8)
                .RequirementSkill(SkillType.TwoHanded, 25)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.PolearmMastery1)

                .AddPerkLevel()
                .Description("Grants +2 BAB when equipped with a Polearm.")
                .Price(8)
                .RequirementSkill(SkillType.TwoHanded, 40)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.PolearmMastery2)

                .AddPerkLevel()
                .Description("Grants +3 BAB when equipped with a Polearm.")
                .Price(8)
                .RequirementSkill(SkillType.TwoHanded, 50)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.PolearmMastery3);
        }

        private void Skewer(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedPolearm, PerkType.Skewer)
                .Name("Skewer")

                .AddPerkLevel()
                .Description("Your next attack deals an additional 1d6 damage and has a 45% chance to interrupt the concentration of your target.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 15)
                .GrantsFeat(FeatType.Skewer1)

                .AddPerkLevel()
                .Description("Your next attack deals an additional 2d6 damage and has a 75% chance to interrupt the concentration of your target.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 30)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.Skewer2)

                .AddPerkLevel()
                .Description("Your next attack deals an additional 3d6 damage and has a 100% chance to interrupt the concentration of your target.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 45)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.Skewer3);
        }

        private void DoubleThrust(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedPolearm, PerkType.DoubleThrust)
                .Name("Double Thrust")

                .AddPerkLevel()
                .Description("Instantly attacks twice, each for 1d4 damage.")
                .Price(2)
                .RequirementSkill(SkillType.TwoHanded, 5)
                .GrantsFeat(FeatType.DoubleThrust1)

                .AddPerkLevel()
                .Description("Instantly attacks twice, each for 2d6 damage.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 20)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.DoubleThrust2)

                .AddPerkLevel()
                .Description("Instantly attacks twice, each for 3d6 damage.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 35)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.DoubleThrust3);
        }

        private void WeaponFocusTwinBlades(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedTwinBlade, PerkType.WeaponFocusTwinBlades)
                .Name("Weapon Focus - Twin Blades")

                .AddPerkLevel()
                .Description("You gain the Weapon Focus feat which grants a +1 attack bonus when equipped with twin blades.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 5)
                .GrantsFeat(FeatType.WeaponFocusTwinBlades)

                .AddPerkLevel()
                .Description("You gain the Weapon Specialization feat which grants a +2 damage when equipped with twin blades.")
                .Price(4)
                .RequirementSkill(SkillType.TwoHanded, 15)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.WeaponSpecializationTwinBlades);
        }

        private void ImprovedCriticalTwinBlades(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedTwinBlade, PerkType.ImprovedCriticalTwinBlades)
                .Name("Improved Critical - Twin Blades")

                .AddPerkLevel()
                .Description("Improves the critical hit chance when using a twin blades.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 25)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.ImprovedCriticalTwinBlades);
        }

        private void TwinBladeProficiency(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedTwinBlade, PerkType.TwinBladeProficiency)
                .Name("Twin Blade Proficiency")

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 1 twin blades.")
                .Price(2)
                .GrantsFeat(FeatType.TwinBladeProficiency1)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 2 twin blades.")
                .Price(2)
                .RequirementSkill(SkillType.TwoHanded, 10)
                .GrantsFeat(FeatType.TwinBladeProficiency2)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 3 twin blades.")
                .Price(2)
                .RequirementSkill(SkillType.TwoHanded, 20)
                .GrantsFeat(FeatType.TwinBladeProficiency3)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 4 twin blades.")
                .Price(2)
                .RequirementSkill(SkillType.TwoHanded, 30)
                .GrantsFeat(FeatType.TwinBladeProficiency4)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 5 twin blades.")
                .Price(2)
                .RequirementSkill(SkillType.TwoHanded, 40)
                .GrantsFeat(FeatType.TwinBladeProficiency5);
        }

        private void TwinBladeMastery(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedTwinBlade, PerkType.TwinBladeMastery)
                .Name("Twin Blade Mastery")
                .TriggerEquippedItem((player, item, slot, type, level) =>
                {
                    if (slot != InventorySlot.RightHand) return;

                    var itemType = GetBaseItemType(item);
                    if (Item.TwinBladeBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) + level;
                        Creature.SetBaseAttackBonus(player, bab);
                    }
                })
                .TriggerUnequippedItem((player, item, slot, type, level) =>
                {
                    if (slot != InventorySlot.RightHand) return;

                    var itemType = GetBaseItemType(item);
                    if (Item.TwinBladeBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) - level;
                        Creature.SetBaseAttackBonus(player, bab);
                    }

                })
                .TriggerPurchase((player, type, level) =>
                {
                    var item = GetItemInSlot(InventorySlot.RightHand, player);
                    var itemType = GetBaseItemType(item);

                    if (Item.TwinBladeBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) + 1;
                        Creature.SetBaseAttackBonus(player, bab);
                    }
                })
                .TriggerRefund((player, type, level) =>
                {
                    var item = GetItemInSlot(InventorySlot.RightHand, player);
                    var itemType = GetBaseItemType(item);

                    if (Item.TwinBladeBaseItemTypes.Contains(itemType))
                    {
                        var bab = GetBaseAttackBonus(player) - level;
                        Creature.SetBaseAttackBonus(player, bab);
                    }
                })

                .AddPerkLevel()
                .Description("Grants +1 BAB when equipped with a Twin Blade.")
                .Price(8)
                .RequirementSkill(SkillType.TwoHanded, 25)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.TwinBladeMastery1)

                .AddPerkLevel()
                .Description("Grants +2 BAB when equipped with a Twin Blade.")
                .Price(8)
                .RequirementSkill(SkillType.TwoHanded, 40)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.TwinBladeMastery2)

                .AddPerkLevel()
                .Description("Grants +3 BAB when equipped with a Twin Blade.")
                .Price(8)
                .RequirementSkill(SkillType.TwoHanded, 50)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.TwinBladeMastery3);
        }

        private void LegSweep(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedTwinBlade, PerkType.LegSweep)
                .Name("Leg Sweep")

                .AddPerkLevel()
                .Description("Your next attack deals an additional 1d8 damage and has a 25% chance to inflict knockdown on your target for 6 seconds.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 15)
                .GrantsFeat(FeatType.LegSweep1)

                .AddPerkLevel()
                .Description("Your next attack deals an additional 2d8 damage and has a 40% chance to inflict knockdown on your target for 6 seconds.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 30)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.LegSweep2)

                .AddPerkLevel()
                .Description("Your next attack deals an additional 3d8 damage and has a 50% chance to inflict knockdown on your target for 6 seconds.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 45)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.LegSweep3);
        }

        private void CrossCut(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.TwoHandedTwinBlade, PerkType.CrossCut)
                .Name("Cross Cut")

                .AddPerkLevel()
                .Description("Instantly attacks twice, each for 1d4. Successful hits reduce your target's AC by 2 for 1 minute.")
                .Price(2)
                .RequirementSkill(SkillType.TwoHanded, 5)
                .GrantsFeat(FeatType.CrossCut1)

                .AddPerkLevel()
                .Description("Instantly attacks twice, each for 2d4. Successful hits reduce your target's AC by 4 for 1 minute.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 20)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.CrossCut2)

                .AddPerkLevel()
                .Description("Instantly attacks twice, each for 3d4. Successful hits reduce your target's AC by 6 for 1 minute.")
                .Price(3)
                .RequirementSkill(SkillType.TwoHanded, 35)
                .RequirementCharacterType(CharacterType.Standard)
                .GrantsFeat(FeatType.CrossCut3);
        }
    }
}
