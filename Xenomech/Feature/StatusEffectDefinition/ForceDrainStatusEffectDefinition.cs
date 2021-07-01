using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.StatusEffectService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.StatusEffectDefinition
{
    public class ForceDrainStatusEffectDefinition : IStatusEffectListDefinition
    {
        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            var builder = new StatusEffectBuilder();
            ForceDrain1(builder);
            ForceDrain2(builder);
            ForceDrain3(builder);
            ForceDrain4(builder);
            ForceDrain5(builder);

            return builder.Build();
        }

        private void ForceDrain1(StatusEffectBuilder builder)
        {
            builder.Create(StatusEffectType.ForceDrain1)
                .Name("Force Drain I")
                .EffectIcon(25) // 25 = Haste
                .TickAction((source, target) =>
                {
                    if (!Ability.GetAbilityResisted(source, target, AbilityType.Intelligence, AbilityType.Wisdom))
                    {
                        ApplyEffectToObject(DurationType.Instant, EffectDamage(1), target);
                        ApplyEffectToObject(DurationType.Instant, EffectHeal(1), target);
                        ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Pulse_Negative), source);
                    }                   

                    Enmity.ModifyEnmityOnAll(source, 1);
                    CombatPoint.AddCombatPointToAllTagged(source, SkillType.Force, 3);
                });
        }
        private void ForceDrain2(StatusEffectBuilder builder)
        {
            builder.Create(StatusEffectType.ForceDrain2)
                .Name("Force Drain I")
                .EffectIcon(25) // 25 = Haste
                .TickAction((source, target) =>
                {
                    if (!Ability.GetAbilityResisted(source, target, AbilityType.Intelligence, AbilityType.Wisdom))
                    {
                        ApplyEffectToObject(DurationType.Instant, EffectDamage(2), target);
                        ApplyEffectToObject(DurationType.Instant, EffectHeal(2), target);
                        ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Pulse_Negative), source);
                    }

                    Enmity.ModifyEnmityOnAll(source, 1);
                    CombatPoint.AddCombatPointToAllTagged(source, SkillType.Force, 3);
                });
        }
        private void ForceDrain3(StatusEffectBuilder builder)
        {
            builder.Create(StatusEffectType.ForceDrain3)
                .Name("Force Drain I")
                .EffectIcon(25) // 25 = Haste
                .TickAction((source, target) =>
                {
                    if (!Ability.GetAbilityResisted(source, target, AbilityType.Intelligence, AbilityType.Wisdom))
                    {
                        ApplyEffectToObject(DurationType.Instant, EffectDamage(3), target);
                        ApplyEffectToObject(DurationType.Instant, EffectHeal(3), target);
                        ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Pulse_Negative), source);
                    }

                    Enmity.ModifyEnmityOnAll(source, 1);
                    CombatPoint.AddCombatPointToAllTagged(source, SkillType.Force, 3);
                });
        }
        private void ForceDrain4(StatusEffectBuilder builder)
        {
            builder.Create(StatusEffectType.ForceDrain4)
                .Name("Force Drain I")
                .EffectIcon(25) // 25 = Haste
                .TickAction((source, target) =>
                {
                    if (!Ability.GetAbilityResisted(source, target, AbilityType.Intelligence, AbilityType.Wisdom))
                    {
                        ApplyEffectToObject(DurationType.Instant, EffectDamage(4), target);
                        ApplyEffectToObject(DurationType.Instant, EffectHeal(4), target);
                        ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Pulse_Negative), source);
                    }

                    Enmity.ModifyEnmityOnAll(source, 1);
                    CombatPoint.AddCombatPointToAllTagged(source, SkillType.Force, 3);
                });
        }
        private void ForceDrain5(StatusEffectBuilder builder)
        {
            builder.Create(StatusEffectType.ForceDrain5)
                .Name("Force Drain I")
                .EffectIcon(25) // 25 = Haste
                .TickAction((source, target) =>
                {
                    if (!Ability.GetAbilityResisted(source, target, AbilityType.Intelligence, AbilityType.Wisdom))
                    {
                        ApplyEffectToObject(DurationType.Instant, EffectDamage(5), target);
                        ApplyEffectToObject(DurationType.Instant, EffectHeal(5), target);
                        ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Pulse_Negative), source);
                    }

                    Enmity.ModifyEnmityOnAll(source, 1);
                    CombatPoint.AddCombatPointToAllTagged(source, SkillType.Force, 3);
                });
        }
    }
}
