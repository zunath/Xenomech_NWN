//using Random = SWLOR.Game.Server.Service.Random;

using System.Collections.Generic;
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

        private static string Validation(uint activator, uint target, int level)
        {
            var weapon = GetItemInSlot(InventorySlot.RightHand, activator);

            if (!Item.PistolBaseItemTypes.Contains(GetBaseItemType(weapon)))
            {
                return "This is a pistol ability.";
            }
            else
                return string.Empty;
        }

        private static void ImpactAction(uint activator, uint target, int level)
        {
            var damage = 0;

            // If activator is in stealth mode, force them out of stealth mode.
            if (GetActionMode(activator, ActionMode.Stealth) == true)
                SetActionMode(activator, ActionMode.Stealth, false);

            switch (level)
            {
                case 1:
                    damage = d4();
                    break;
                case 2:
                    damage = d6(2);
                    break;
                case 3:
                    damage = d6(3);
                    break;
                default:
                    break;
            }

            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Piercing), target);

            Enmity.ModifyEnmityOnAll(activator, 1);
            CombatPoint.AddCombatPointToAllTagged(activator, SkillType.Elemental, 3);
        }

        private static void DoubleShot1(AbilityBuilder builder)
        {
            builder.Create(FeatType.DoubleShot1, PerkType.DoubleShot)
                .Name("Double Shot I")
                .HasRecastDelay(RecastGroup.DoubleShot, 60f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(3)
                .IsCastedAbility()
                .HasCustomValidation((activator, target, level) =>
                {
                    return Validation(activator, target, level);
                })
                .HasImpactAction((activator, target, level) =>
                {
                    ImpactAction(activator, target, level);
                    ImpactAction(activator, target, level);
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
                .HasCustomValidation((activator, target, level) =>
                {
                    return Validation(activator, target, level);
                })
                .HasImpactAction((activator, target, level) =>
                {
                    ImpactAction(activator, target, level);
                    ImpactAction(activator, target, level);
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
                .HasCustomValidation((activator, target, level) =>
                {
                    return Validation(activator, target, level);
                })
                .HasImpactAction((activator, target, level) =>
                {
                    ImpactAction(activator, target, level);
                    ImpactAction(activator, target, level);
                });
        }
    }
}