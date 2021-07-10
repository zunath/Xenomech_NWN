using System.Collections.Generic;
using Xenomech.Enumeration;
using Xenomech.Service.StatusEffectService;

namespace Xenomech.Feature.StatusEffectDefinition
{
    public class SpiritProtectionStatusEffectDefinition: IStatusEffectListDefinition
    {
        private readonly StatusEffectBuilder _builder = new StatusEffectBuilder();

        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            SpiritProtection1();
            SpiritProtection2();
            SpiritProtection3();

            return _builder.Build();
        }

        private void SpiritProtection1()
        {
            _builder.Create(StatusEffectType.SpiritProtection1)
                .Name("Spirit Protection I")
                .EffectIcon(1); // todo icon
        }

        private void SpiritProtection2()
        {
            _builder.Create(StatusEffectType.SpiritProtection2)
                .Name("Spirit Protection II")
                .EffectIcon(1); // todo icon
        }

        private void SpiritProtection3()
        {
            _builder.Create(StatusEffectType.SpiritProtection3)
                .Name("Spirit Protection III")
                .EffectIcon(1); // todo icon
        }
    }
}
