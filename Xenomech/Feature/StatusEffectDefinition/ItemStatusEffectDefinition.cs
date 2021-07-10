using System.Collections.Generic;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.StatusEffectService;

namespace Xenomech.Feature.StatusEffectDefinition
{
    public class ItemStatusEffectDefinition: IStatusEffectListDefinition
    {
        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            var builder = new StatusEffectBuilder();
            EnergyPackEffect(builder);
            
            return builder.Build();
        }

        private void EnergyPackEffect(StatusEffectBuilder builder)
        {
            void CreateEffect(string name, int amount)
            {
                builder.Create(StatusEffectType.EtherPack1)
                    .Name(name)
                    .EffectIcon(2) // 2 = Regenerate
                    .TickAction((source, target) =>
                    {
                        Stat.RestoreEP(target, amount);
                    });
            }

            CreateEffect("Energy Pack I", 2);
            CreateEffect("Energy Pack II", 3);
            CreateEffect("Energy Pack III", 4);
            CreateEffect("Energy Pack IV", 5);
            CreateEffect("Energy Pack V", 6);
        }
    }
}
