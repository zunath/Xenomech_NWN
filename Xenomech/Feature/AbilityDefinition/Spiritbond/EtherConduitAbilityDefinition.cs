using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.AbilityDefinition.Spiritbond
{
    public class EtherConduitAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            EtherConduit();

            return _builder.Build();
        }

        private void EtherConduit()
        {
            _builder.Create(FeatType.EtherConduit, PerkType.EtherConduit)
                .Name("Ether Conduit")
                .HasRecastDelay(RecastGroup.EtherConduit, 300f)
                .IsCastedAbility()
                .UsesAnimation(Animation.LoopingConjure2)
                .HasImpactAction((activator, _, _, targetLocation) =>
                {
                    StatusEffect.Apply(activator, activator, StatusEffectType.EtherConduit, 15f);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Death_Ward), activator);

                    Enmity.ModifyEnmityOnAll(activator, 30);
                });
        }

    }
}
