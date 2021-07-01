using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Enumeration;
using Xenomech.Service.AbilityService;
using static Xenomech.Core.NWScript.NWScript;
using Random = Xenomech.Service.Random;

namespace Xenomech.Feature.AbilityDefinition.MartialArts
{
    public class KnockdownAbilityDefinition: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Knockdown();

            return _builder.Build();
        }

        private void Knockdown()
        {
            _builder.Create(FeatType.Knockdown, PerkType.Knockdown)
                .Name("Knockdown")
                .HasRecastDelay(RecastGroup.Knockdown, 60f)
                .IsWeaponAbility()
                .RequirementStamina(6)
                .HasImpactAction((activator, target, level) =>
                {
                    var isHit = Random.D100(1) <= 60;
                    if (!isHit) return;

                    ApplyEffectToObject(DurationType.Temporary, EffectKnockdown(), target, 12f);
                });
        }
    }
}
