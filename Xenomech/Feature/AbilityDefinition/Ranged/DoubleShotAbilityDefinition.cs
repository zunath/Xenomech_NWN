using System.Collections.Generic;
using Xenomech.Core;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.Ranged
{
    public class DoubleShotAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            DoubleShot1(builder);
            DoubleShot2(builder);
            DoubleShot3(builder);

            return builder.Build();
        }

        private static string Validation(uint activator, uint target, int level, Location targetLocation)
        {
            var weapon = GetItemInSlot(InventorySlot.RightHand, activator);

            if (!Item.PistolBaseItemTypes.Contains(GetBaseItemType(weapon)))
            {
                return "This is a pistol ability.";
            }
            else
                return string.Empty;
        }

        private static void ImpactAction(uint activator, uint target, int level, Location targetLocation)
        {
            var dmg = 0.0f;

            // If activator is in stealth mode, force them out of stealth mode.
            if (GetActionMode(activator, ActionMode.Stealth) == true)
                SetActionMode(activator, ActionMode.Stealth, false);

            switch (level)
            {
                case 1:
                    dmg = 4.5f;
                    break;
                case 2:
                    dmg = 8.5f;
                    break;
                case 3:
                    dmg = 11.5f;
                    break;
                default:
                    break;
            }

            // First attack
            var perception = GetAbilityModifier(AbilityType.Perception, activator);
            var defense = Combat.CalculateDefense(target);
            var vitality = GetAbilityModifier(AbilityType.Vitality, target);
            var damage = Combat.CalculateDamage(dmg, perception, defense, vitality, false);
            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Piercing), target);

            // Second attack
            damage = Combat.CalculateDamage(dmg, perception, defense, vitality, false);
            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Piercing), target);

            CombatPoint.AddCombatPoint(activator, target, SkillType.Ranged, 3);
        }

        private static void DoubleShot1(AbilityBuilder builder)
        {
            builder.Create(FeatType.DoubleShot1, PerkType.DoubleShot)
                .Name("Double Shot I")
                .HasRecastDelay(RecastGroup.DoubleShot, 60f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(3)
                .IsCastedAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction((activator, target, level, targetLocation) =>
                {
                    ImpactAction(activator, target, level, targetLocation);
                    ImpactAction(activator, target, level, targetLocation);
                });
        }
        private static void DoubleShot2(AbilityBuilder builder)
        {
            builder.Create(FeatType.DoubleShot2, PerkType.DoubleShot)
                .Name("Double Shot II")
                .HasRecastDelay(RecastGroup.DoubleShot, 60f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(5)
                .IsCastedAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction((activator, target, level, targetLocation) =>
                {
                    ImpactAction(activator, target, level, targetLocation);
                    ImpactAction(activator, target, level, targetLocation);
                });
        }
        private static void DoubleShot3(AbilityBuilder builder)
        {
            builder.Create(FeatType.DoubleShot3, PerkType.DoubleShot)
                .Name("Double Shot III")
                .HasRecastDelay(RecastGroup.DoubleShot, 60f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(8)
                .IsCastedAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction((activator, target, level, targetLocation) =>
                {
                    ImpactAction(activator, target, level, targetLocation);
                    ImpactAction(activator, target, level, targetLocation);
                });
        }
    }
}