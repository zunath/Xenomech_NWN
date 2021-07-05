using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Service.ImplantService;

namespace Xenomech.Feature.ImplantDefinition
{
    public class DexterousImplantDefinition: IImplantListDefinition
    {
        private readonly ImplantBuilder _builder = new ImplantBuilder();

        public Dictionary<string, ImplantDetail> BuildImplants()
        {
            DexterousImplant1();
            DexterousImplant2();
            DexterousImplant3();
            DexterousImplant4();
            DexterousImplant5();

            return _builder.Build();
        }


        private void DexterousImplant1()
        {
            _builder.Create("h_imp_dex1")
                .Name("Dexterous")
                .Description("+1 DEX, -1 INT")
                .RequiredLevel(1)
                .Slot(ImplantSlotType.Legs)
                .ModifyAbilityScore(AbilityType.Perception, 1)
                .ModifyAbilityScore(AbilityType.Unused, -1);
        }

        private void DexterousImplant2()
        {
            _builder.Create("h_imp_dex2")
                .Name("Dexterous")
                .Description("+2 DEX, -1 WIS, -1 INT")
                .RequiredLevel(2)
                .Slot(ImplantSlotType.Legs)
                .ModifyAbilityScore(AbilityType.Perception, 2)
                .ModifyAbilityScore(AbilityType.Unused, -1)
                .ModifyAbilityScore(AbilityType.Spirit, -1);
        }
        private void DexterousImplant3()
        {
            _builder.Create("h_imp_dex3")
                .Name("Dexterous")
                .Description("+3 DEX, -2 WIS, -2 INT")
                .RequiredLevel(3)
                .Slot(ImplantSlotType.Legs)
                .ModifyAbilityScore(AbilityType.Perception, 3)
                .ModifyAbilityScore(AbilityType.Unused, -2)
                .ModifyAbilityScore(AbilityType.Spirit, -2);
        }
        private void DexterousImplant4()
        {
            _builder.Create("h_imp_dex4")
                .Name("Dexterous")
                .Description("+4 DEX, -3 WIS, -3 INT")
                .RequiredLevel(4)
                .Slot(ImplantSlotType.Legs)
                .ModifyAbilityScore(AbilityType.Perception, 4)
                .ModifyAbilityScore(AbilityType.Unused, -3)
                .ModifyAbilityScore(AbilityType.Spirit, -3);
        }
        private void DexterousImplant5()
        {
            _builder.Create("h_imp_dex5")
                .Name("Dexterous")
                .Description("+5 DEX, -4 WIS, -4 INT")
                .RequiredLevel(5)
                .Slot(ImplantSlotType.Legs)
                .ModifyAbilityScore(AbilityType.Perception, 5)
                .ModifyAbilityScore(AbilityType.Unused, -4)
                .ModifyAbilityScore(AbilityType.Spirit, -4);
        }
    }
}
