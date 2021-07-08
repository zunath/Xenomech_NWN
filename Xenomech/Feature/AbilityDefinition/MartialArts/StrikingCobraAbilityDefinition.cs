using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.MartialArts
{
    public class StrikingCobraAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            StrikingCobra1(builder);
            StrikingCobra2(builder);
            StrikingCobra3(builder);

            return builder.Build();
        }

        private static string Validation(uint activator, uint target, int level)
        {
            var weapon = GetItemInSlot(InventorySlot.RightHand, activator);

            if (!Item.KnucklesBaseItemTypes.Contains(GetBaseItemType(weapon)))
            {
                return "This is a knuckles ability.";
            }
            else
                return string.Empty;
        }

        private static void ImpactAction(uint activator, uint target, int level)
        {
            var dmg = 0.0f;
            var duration = 0f;
            var inflict = false;
            // If activator is in stealth mode, force them out of stealth mode.
            if (GetActionMode(activator, ActionMode.Stealth) == true)
                SetActionMode(activator, ActionMode.Stealth, false);

            switch (level)
            {
                case 1:
                    dmg = 6.5f;
                    inflict = true;
                    duration = 30f;
                    break;
                case 2:
                    dmg = 11.5f;
                    inflict = true;
                    duration = 60f;
                    break;
                case 3:
                    dmg = 16.5f;
                    duration = 60f;
                    break;
                default:
                    break;
            }

            var perception = GetAbilityModifier(AbilityType.Perception, activator);
            var defense = Combat.CalculateDefense(target);
            var vitality = GetAbilityModifier(AbilityType.Vitality, target);
            var damage = Combat.CalculateDamage(dmg, perception, defense, vitality, false);
            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Bludgeoning), target);
            if (inflict) StatusEffect.Apply(activator, target, StatusEffectType.Poison, duration);

            CombatPoint.AddCombatPoint(activator, target, SkillType.MartialArts, 3);
        }

        private static void StrikingCobra1(AbilityBuilder builder)
        {
            builder.Create(FeatType.StrikingCobra1, PerkType.StrikingCobra)
                .Name("Striking Cobra I")
                .HasRecastDelay(RecastGroup.StrikingCobra, 60f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(3)
                .IsWeaponAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void StrikingCobra2(AbilityBuilder builder)
        {
            builder.Create(FeatType.StrikingCobra2, PerkType.StrikingCobra)
                .Name("Striking Cobra II")
                .HasRecastDelay(RecastGroup.StrikingCobra, 60f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(5)
                .IsWeaponAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void StrikingCobra3(AbilityBuilder builder)
        {
            builder.Create(FeatType.StrikingCobra3, PerkType.StrikingCobra)
                .Name("Striking Cobra III")
                .HasRecastDelay(RecastGroup.StrikingCobra, 60f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(8)
                .IsWeaponAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
    }
}