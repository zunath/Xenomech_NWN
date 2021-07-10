using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.Arcane
{
    public class ConvertAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Convert();

            return _builder.Build();
        }

        private void Convert()
        {
            _builder.Create(FeatType.Convert, PerkType.Convert)
                .Name("Convert")
                .HasRecastDelay(RecastGroup.Convert, 300f)
                .IsCastedAbility()
                .HasActivationDelay(1f)
                .UsesAnimation(Animation.LoopingConjure2)
                .HasCustomValidation((activator, target, level) =>
                {
                    var hp = GetCurrentHitPoints(activator);
                    var ep = Stat.GetCurrentEP(activator);

                    if (ep <= 0)
                    {
                        return "Your EP is too low to convert.";
                    }

                    if (hp == ep)
                    {
                        return "HP and EP values are the same. Convert failed.";
                    }

                    return string.Empty;
                })
                .HasImpactAction((activator, target, _) =>
                {
                    var hp = GetCurrentHitPoints(activator);
                    var ep = Stat.GetCurrentEP(activator);

                    // HP is greater than EP, reduce HP and heal EP.
                    if (hp > ep)
                    {
                        var damageAmount = hp - ep;
                        ApplyEffectToObject(DurationType.Instant, EffectDamage(damageAmount), activator);

                        var epRestore = ep - hp;
                        Stat.RestoreEP(target, epRestore);
                    }
                    // HP is less than EP, heal HP and reduce EP.
                    else if (hp < ep)
                    {
                        var recoverAmount = ep - hp;
                        ApplyEffectToObject(DurationType.Instant, EffectHeal(recoverAmount), activator);

                        var epReduce = hp - ep;
                        Stat.ReduceEP(activator, epReduce);
                    }

                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Knock), target);
                });
        }
    }
}
