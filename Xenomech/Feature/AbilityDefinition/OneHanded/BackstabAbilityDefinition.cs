﻿using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.Item;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.OneHanded
{
    public class BackstabAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            Backstab1(builder);
            Backstab2(builder);
            Backstab3(builder);

            return builder.Build();
        }

        private static string Validation(uint activator, uint target, int level)
        {
            var weapon = GetItemInSlot(InventorySlot.RightHand, activator);

            if (Item.FinesseVibrobladeBaseItemTypes.Contains(GetBaseItemType(weapon))
                && (GetBaseItemType((GetItemInSlot(InventorySlot.LeftHand))) == BaseItem.SmallShield ||
                    GetBaseItemType((GetItemInSlot(InventorySlot.LeftHand))) == BaseItem.LargeShield ||
                    GetBaseItemType((GetItemInSlot(InventorySlot.LeftHand))) == BaseItem.TowerShield ||
                    GetBaseItemType((GetItemInSlot(InventorySlot.LeftHand))) == BaseItem.Invalid))
            {
                return "This is a one-handed ability.";
            }
            else
                return string.Empty;
        }

        private static void ImpactAction(uint activator, uint target, int level)
        {
            var dmg = 0.0f;

            // If activator is in stealth mode, force them out of stealth mode.
            if (GetActionMode(activator, ActionMode.Stealth) == true)
                SetActionMode(activator, ActionMode.Stealth, false);

            switch (level)
            {
                case 1:
                    dmg = 8.0f;
                    break;
                case 2:
                    dmg = 13.0f;
                    break;
                case 3:
                    dmg = 18.0f;
                    break;
                default:
                    break;
            }

            if (abs((int) (GetFacing(activator) - GetFacing(target))) > 200f ||
                abs((int)(GetFacing(activator) - GetFacing(target))) < 160f ||
                GetDistanceBetween(activator, target) > 5f)
            {
                dmg /= 2;
            }

            var perception = GetAbilityModifier(AbilityType.Perception, activator);
            var defense = Combat.CalculateDefense(target);
            var vitality = GetAbilityModifier(AbilityType.Vitality, target);
            var damage = Combat.CalculateDamage(dmg, perception, defense, vitality, false);
            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Slashing), target);

            CombatPoint.AddCombatPoint(activator, target, SkillType.OneHanded, 3);
        }

        private static void Backstab1(AbilityBuilder builder)
        {
            builder.Create(FeatType.Backstab1, PerkType.Backstab)
                .Name("Backstab I")
                .HasRecastDelay(RecastGroup.Backstab, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(3)
                .IsCastedAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void Backstab2(AbilityBuilder builder)
        {
            builder.Create(FeatType.Backstab2, PerkType.Backstab)
                .Name("Backstab II")
                .HasRecastDelay(RecastGroup.Backstab, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(5)
                .IsCastedAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void Backstab3(AbilityBuilder builder)
        {
            builder.Create(FeatType.Backstab3, PerkType.Backstab)
                .Name("Backstab III")
                .HasRecastDelay(RecastGroup.Backstab, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(8)
                .IsCastedAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
    }
}