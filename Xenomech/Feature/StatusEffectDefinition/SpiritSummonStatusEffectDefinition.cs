using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.Associate;
using Xenomech.Core.NWScript.Enum.VisualEffect;
using Xenomech.Enumeration;
using Xenomech.Service;
using Xenomech.Service.StatusEffectService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature.StatusEffectDefinition
{
    public class SpiritSummonStatusEffectDefinition: IStatusEffectListDefinition
    {
        private static readonly StatusEffectBuilder _builder = new StatusEffectBuilder();

        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            SpiritOfRage1();
            SpiritOfRage2();
            SpiritOfRage3();

            SpiritOfChange1();
            SpiritOfChange2();
            SpiritOfChange3();

            SpiritOfFaith1();
            SpiritOfFaith2();
            SpiritOfFaith3();

            return _builder.Build();
        }

        private void GrantAction(uint activator, string summonResref)
        {
            AssignCommand(activator, () =>
            {
                var effect = EffectSummonCreature(summonResref, VisualEffect.Vfx_Imp_Unsummon);
                var location = GetLocation(activator);
                ApplyEffectAtLocation(DurationType.Permanent, effect, location);

                Enmity.ModifyEnmityOnAll(activator, 40);
            });
        }

        private void TickAction(uint source, StatusEffectType statusEffect)
        {
            var associate = GetAssociate(AssociateType.Summoned, source);

            // Associate is dead or gone. Remove status effect.
            if (!GetIsObjectValid(associate))
            {
                StatusEffect.Remove(source, statusEffect);
            }
        }

        private void RemoveAction(uint target)
        {
            var associate = GetAssociate(AssociateType.Summoned, target);
            
            if (GetIsObjectValid(associate))
            {
                var location = GetLocation(associate);
                ApplyEffectAtLocation(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Unsummon), location);
                DestroyObject(associate);
            }
        }

        private void SpiritOfRage1()
        {
            _builder.Create(StatusEffectType.SpiritOfRage1)
                .Name("Spirit of Rage I")
                .EffectIcon(1) // todo icon
                .GrantAction((source, _, _) =>
                {
                    GrantAction(source, "rage_spirit1");
                })
                .TickAction((source, _) =>
                {
                    TickAction(source, StatusEffectType.SpiritOfRage1);
                })
                .RemoveAction(RemoveAction);
        }

        private void SpiritOfRage2()
        {
            _builder.Create(StatusEffectType.SpiritOfRage2)
                .Name("Spirit of Rage II")
                .EffectIcon(1) // todo icon
                .GrantAction((source, _, _) =>
                {
                    GrantAction(source, "rage_spirit2");
                })
                .TickAction((source, _) =>
                {
                    TickAction(source, StatusEffectType.SpiritOfRage2);
                })
                .RemoveAction(RemoveAction);
        }

        private void SpiritOfRage3()
        {
            _builder.Create(StatusEffectType.SpiritOfRage3)
                .Name("Spirit of Rage III")
                .EffectIcon(1) // todo icon
                .GrantAction((source, _, _) =>
                {
                    GrantAction(source, "rage_spirit3");
                })
                .TickAction((source, _) =>
                {
                    TickAction(source, StatusEffectType.SpiritOfRage3);
                })
                .RemoveAction(RemoveAction);
        }

        private void SpiritOfChange1()
        {
            _builder.Create(StatusEffectType.SpiritOfChange1)
                .Name("Spirit of Change I")
                .EffectIcon(1) // todo icon
                .GrantAction((source, _, _) =>
                {
                    GrantAction(source, "change_spirit1");
                })
                .TickAction((source, _) =>
                {
                    TickAction(source, StatusEffectType.SpiritOfChange1);
                })
                .RemoveAction(RemoveAction);
        }

        private void SpiritOfChange2()
        {
            _builder.Create(StatusEffectType.SpiritOfChange2)
                .Name("Spirit of Change II")
                .EffectIcon(1) // todo icon
                .GrantAction((source, _, _) =>
                {
                    GrantAction(source, "change_spirit2");
                })
                .TickAction((source, _) =>
                {
                    TickAction(source, StatusEffectType.SpiritOfChange2);
                })
                .RemoveAction(RemoveAction);
        }

        private void SpiritOfChange3()
        {
            _builder.Create(StatusEffectType.SpiritOfChange3)
                .Name("Spirit of Change III")
                .EffectIcon(1) // todo icon
                .GrantAction((source, _, _) =>
                {
                    GrantAction(source, "change_spirit3");
                })
                .TickAction((source, _) =>
                {
                    TickAction(source, StatusEffectType.SpiritOfChange3);
                })
                .RemoveAction(RemoveAction);
        }

        private void SpiritOfFaith1()
        {
            _builder.Create(StatusEffectType.SpiritOfFaith1)
                .Name("Spirit of Faith I")
                .EffectIcon(1) // todo icon
                .GrantAction((source, _, _) =>
                {
                    GrantAction(source, "faith_spirit1");
                })
                .TickAction((source, _) =>
                {
                    TickAction(source, StatusEffectType.SpiritOfFaith1);
                })
                .RemoveAction(RemoveAction);
        }

        private void SpiritOfFaith2()
        {
            _builder.Create(StatusEffectType.SpiritOfFaith2)
                .Name("Spirit of Faith II")
                .EffectIcon(1) // todo icon
                .GrantAction((source, _, _) =>
                {
                    GrantAction(source, "faith_spirit2");
                })
                .TickAction((source, _) =>
                {
                    TickAction(source, StatusEffectType.SpiritOfFaith2);
                })
                .RemoveAction(RemoveAction);
        }

        private void SpiritOfFaith3()
        {
            _builder.Create(StatusEffectType.SpiritOfFaith3)
                .Name("Spirit of Faith III")
                .EffectIcon(1) // todo icon
                .GrantAction((source, _, _) =>
                {
                    GrantAction(source, "faith_spirit3");
                })
                .TickAction((source, _) =>
                {
                    TickAction(source, StatusEffectType.SpiritOfFaith3);
                })
                .RemoveAction(RemoveAction);
        }
    }
}
