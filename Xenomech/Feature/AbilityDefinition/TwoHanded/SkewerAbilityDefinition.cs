using System.Collections.Generic;
using Xenomech.Core;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.TwoHanded
{
    public class SkewerAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            Skewer1(builder);
            Skewer2(builder);
            Skewer3(builder);

            return builder.Build();
        }

        private static string Validation(uint activator, uint target, int level, Location targetLocation)
        {
            var weapon = GetItemInSlot(InventorySlot.RightHand, activator);

            if (!Item.PolearmBaseItemTypes.Contains(GetBaseItemType(weapon)))
            {
                return "This is a polearm ability.";
            }
            else 
                return string.Empty;
        }

        private static void ImpactAction(uint activator, uint target, int level, Location targetLocation)
        {
            var dmg = 0.0f;
            var inflict = false;
            // If activator is in stealth mode, force them out of stealth mode.
            if (GetActionMode(activator, ActionMode.Stealth) == true)
                SetActionMode(activator, ActionMode.Stealth, false);

            switch (level)
            {
                case 1:
                    dmg = 6.5f;
                    if (Random(100)<45) inflict = true;
                    break;
                case 2:
                    dmg = 11.5f;
                    if (d4()>1) inflict = true;
                    break;
                case 3:
                    dmg = 16.5f;
                    inflict = true;
                    break;
                default:
                    break;
            }

            var perception = GetAbilityModifier(AbilityType.Might, activator);
            var defense = Combat.CalculateDefense(target);
            var vitality = GetAbilityModifier(AbilityType.Vitality, target);
            var damage = Combat.CalculateDamage(dmg, perception, defense, vitality, false);
            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Piercing), target);
            if (inflict)
            {
                Ability.EndConcentrationAbility(target);
                SendMessageToPC(activator, ColorToken.Gray(GetName(target)) + "'s  concentration has been broken.");
                SendMessageToPC(target, ColorToken.Gray(GetName(activator)) + " broke your concentration.");
            }

            CombatPoint.AddCombatPoint(activator, target, SkillType.TwoHanded, 3);
        }

        private static void Skewer1(AbilityBuilder builder)
        {
            builder.Create(FeatType.Skewer1, PerkType.Skewer)
                .Name("Skewer I")
                .HasRecastDelay(RecastGroup.Skewer, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(3)
                .IsWeaponAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void Skewer2(AbilityBuilder builder)
        {
            builder.Create(FeatType.Skewer2, PerkType.Skewer)
                .Name("Skewer II")
                .HasRecastDelay(RecastGroup.Skewer, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(4)
                .IsWeaponAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void Skewer3(AbilityBuilder builder)
        {
            builder.Create(FeatType.Skewer3, PerkType.Skewer)
                .Name("Skewer III")
                .HasRecastDelay(RecastGroup.Skewer, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(5)
                .IsWeaponAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
    }
}