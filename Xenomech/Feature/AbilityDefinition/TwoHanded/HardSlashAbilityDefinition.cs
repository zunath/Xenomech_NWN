using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.TwoHanded
{
    public class HardSlashAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            HardSlash1(builder);
            HardSlash2(builder);
            HardSlash3(builder);

            return builder.Build();
        }

        private static string Validation(uint activator, uint target, int level)
        {
            var weapon = GetItemInSlot(InventorySlot.RightHand, activator);

            if (!Item.HeavyVibrobladeBaseItemTypes.Contains(GetBaseItemType(weapon)))
            {
                return "This is a heavy vibroblade ability.";
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
                    dmg = 7.0f;
                    break;
                case 2:
                    dmg = 12.0f;
                    break;
                case 3:
                    dmg = 17.0f;
                    break;
                default:
                    break;
            }

            var might = GetAbilityModifier(AbilityType.Might, activator);
            var defense = Combat.CalculateDefense(target);
            var vitality = GetAbilityModifier(AbilityType.Vitality, target);
            var damage = Combat.CalculateDamage(dmg, might, defense, vitality, false);
            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Slashing), target);
            CombatPoint.AddCombatPoint(activator, target, SkillType.TwoHanded, 3);
            Enmity.ModifyEnmity(activator, target, 25);
        }

        private static void HardSlash1(AbilityBuilder builder)
        {
            builder.Create(FeatType.HardSlash1, PerkType.HardSlash)
                .Name("Hard Slash I")
                .HasRecastDelay(RecastGroup.HardSlash, 60f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(3)
                .IsCastedAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void HardSlash2(AbilityBuilder builder)
        {
            builder.Create(FeatType.HardSlash2, PerkType.HardSlash)
                .Name("Hard Slash II")
                .HasRecastDelay(RecastGroup.HardSlash, 60f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(5)
                .IsCastedAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void HardSlash3(AbilityBuilder builder)
        {
            builder.Create(FeatType.HardSlash3, PerkType.HardSlash)
                .Name("Hard Slash III")
                .HasRecastDelay(RecastGroup.HardSlash, 60f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(8)
                .IsCastedAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
    }
}