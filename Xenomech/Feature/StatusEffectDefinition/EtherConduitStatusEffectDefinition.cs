using System.Collections.Generic;
using Xenomech.Enumeration;
using Xenomech.Service.StatusEffectService;

namespace Xenomech.Feature.StatusEffectDefinition
{
    public class EtherConduitStatusEffectDefinition : IStatusEffectListDefinition
    {
        private static readonly StatusEffectBuilder _builder = new StatusEffectBuilder();

        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            EtherConduit();

            return _builder.Build();
        }

        private void EtherConduit()
        {
            _builder.Create(StatusEffectType.EtherConduit)
                .EffectIcon(1) // todo icon
                .Name("Ether Conduit");
        }
    }
}
