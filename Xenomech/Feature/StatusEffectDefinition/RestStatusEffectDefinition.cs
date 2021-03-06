using System;
using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.StatusEffectService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.StatusEffectDefinition
{
    public class RestStatusEffectDefinition: IStatusEffectListDefinition
    {
        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            var builder = new StatusEffectBuilder();
            Rest(builder);

            return builder.Build();
        }

        private void Rest(StatusEffectBuilder builder)
        {
            builder.Create(StatusEffectType.Rest)
                .Name("Rest")
                .EffectIcon(8) // 8 = Fatigue
                .GrantAction((source, target, length) =>
                {
                    // Store position the player is at when the rest effect is granted.
                    var position = GetPosition(target);

                    SetLocalFloat(target, "REST_POSITION_X", position.X);
                    SetLocalFloat(target, "REST_POSITION_Y", position.Y);
                    SetLocalFloat(target, "REST_POSITION_Z", position.Z);
                })
                .TickAction((source, target) =>
                {
                    var position = GetPosition(target);

                    var originalPosition = Vector3(
                        GetLocalFloat(target, "REST_POSITION_X"),
                        GetLocalFloat(target, "REST_POSITION_Y"),
                        GetLocalFloat(target, "REST_POSITION_Z"));

                    // Player has moved since the effect started. Remove it.
                    if(Math.Abs(position.X - originalPosition.X) > 0.1f ||
                       Math.Abs(position.Y - originalPosition.Y) > 0.1f ||
                       Math.Abs(position.Z - originalPosition.Z) > 0.1f)
                    {
                        StatusEffect.Remove(target, StatusEffectType.Rest);
                        return;
                    }

                    var hpAmount = (int)(GetMaxHitPoints(target) * 0.1f);
                    var epAmount = (int) (Stat.GetMaxEP(target) * 0.1f);
                    var stmAmount = (int) (Stat.GetMaxStamina(target) * 0.1f);

                    if (hpAmount < 1)
                        hpAmount = 1;
                    if (epAmount < 1)
                        epAmount = 1;
                    if (stmAmount < 1)
                        stmAmount = 1;

                    ApplyEffectToObject(DurationType.Instant, EffectHeal(hpAmount), target);
                    Stat.RestoreStamina(target, stmAmount);
                    Stat.RestoreEP(target, epAmount);
                })
                .RemoveAction(target =>
                {
                    // Clean up position information.
                    DeleteLocalFloat(target, "REST_POSITION_X");
                    DeleteLocalFloat(target, "REST_POSITION_Y");
                    DeleteLocalFloat(target, "REST_POSITION_Z");
                });
        }
    }
}
