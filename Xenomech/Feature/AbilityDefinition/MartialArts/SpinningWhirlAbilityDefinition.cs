using System.Collections.Generic;
using Xenomech.Core;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.MartialArts
{
    public class SpinningWhirlAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            SpinningWhirl1(builder);
            SpinningWhirl2(builder);
            SpinningWhirl3(builder);

            return builder.Build();
        }

        private static string Validation(uint activator, uint target, int level, Location targetLocation)
        {
            var weapon = GetItemInSlot(InventorySlot.RightHand, activator);

            if (!Item.StaffBaseItemTypes.Contains(GetBaseItemType(weapon)))
            {
                return "This is a staff ability.";
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
                    dmg = 5.5f;
                    break;
                case 2:
                    dmg = 10.5f;
                    break;
                case 3:
                    dmg = 15.5f;
                    break;
                default:
                    break;
            }

            var count = 0;
            var creature = GetFirstObjectInShape(Shape.Sphere, RadiusSize.Small, GetLocation(activator), true, ObjectType.Creature);
            while (GetIsObjectValid(creature) && count < 3)
            {

                var might = GetAbilityModifier(AbilityType.Might, activator);
                var defense = Combat.CalculateDefense(creature);
                var vitality = GetAbilityModifier(AbilityType.Vitality, creature);
                var damage = Combat.CalculateDamage(dmg, might, defense, vitality, false);
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Bludgeoning), target);

                CombatPoint.AddCombatPoint(activator, creature, SkillType.MartialArts, 2);
                creature = GetNextObjectInShape(Shape.Sphere, RadiusSize.Small, GetLocation(activator), true, ObjectType.Creature);
                count++;
            }
        }

        private static void SpinningWhirl1(AbilityBuilder builder)
        {
            builder.Create(FeatType.SpinningWhirl1, PerkType.SpinningWhirl)
                .Name("Spinning Whirl I")
                .HasRecastDelay(RecastGroup.SpinningWhirl, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(3)
                .IsCastedAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void SpinningWhirl2(AbilityBuilder builder)
        {
            builder.Create(FeatType.SpinningWhirl2, PerkType.SpinningWhirl)
                .Name("Spinning Whirl II")
                .HasRecastDelay(RecastGroup.SpinningWhirl, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(4)
                .IsCastedAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void SpinningWhirl3(AbilityBuilder builder)
        {
            builder.Create(FeatType.SpinningWhirl3, PerkType.SpinningWhirl)
                .Name("Spinning Whirl III")
                .HasRecastDelay(RecastGroup.SpinningWhirl, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(5)
                .IsCastedAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
    }
}