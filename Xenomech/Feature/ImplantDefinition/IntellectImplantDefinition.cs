using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Service.ImplantService;

namespace Xenomech.Feature.ImplantDefinition
{
    public class IntellectImplantDefinition: IImplantListDefinition
    {
        private readonly ImplantBuilder _builder = new ImplantBuilder();

        public Dictionary<string, ImplantDetail> BuildImplants()
        {
            IntellectImplant1();
            IntellectImplant2();
            IntellectImplant3();
            IntellectImplant4();
            IntellectImplant5();

            return _builder.Build();
        }


        private void IntellectImplant1()
        {
            _builder.Create("h_imp_int1")
                .Name("Intellect")
                .Description("+1 INT, -1 STR")
                .RequiredLevel(1)
                .Slot(ImplantSlotType.Head)
                .ModifyAbilityScore(AbilityType.Unused, 1)
                .ModifyAbilityScore(AbilityType.Might, -1);
        }

        private void IntellectImplant2()
        {
            _builder.Create("h_imp_int2")
                .Name("Intellect")
                .Description("+2 INT, -1 CON, -1 STR")
                .RequiredLevel(2)
                .Slot(ImplantSlotType.Head)
                .ModifyAbilityScore(AbilityType.Unused, 2)
                .ModifyAbilityScore(AbilityType.Vitality, -1)
                .ModifyAbilityScore(AbilityType.Might, -1);
        }
        private void IntellectImplant3()
        {
            _builder.Create("h_imp_int3")
                .Name("Intellect")
                .Description("+3 INT, -2 CON, -2 STR")
                .RequiredLevel(3)
                .Slot(ImplantSlotType.Head)
                .ModifyAbilityScore(AbilityType.Unused, 3)
                .ModifyAbilityScore(AbilityType.Vitality, -2)
                .ModifyAbilityScore(AbilityType.Might, -2);
        }
        private void IntellectImplant4()
        {
            _builder.Create("h_imp_int4")
                .Name("Intellect")
                .Description("+4 INT, -3 CON, -3 STR")
                .RequiredLevel(4)
                .Slot(ImplantSlotType.Head)
                .ModifyAbilityScore(AbilityType.Unused, 4)
                .ModifyAbilityScore(AbilityType.Vitality, -3)
                .ModifyAbilityScore(AbilityType.Might, -3);
        }
        private void IntellectImplant5()
        {
            _builder.Create("h_imp_int5")
                .Name("Intellect")
                .Description("+5 INT, -4 CON, -4 STR")
                .RequiredLevel(5)
                .Slot(ImplantSlotType.Head)
                .ModifyAbilityScore(AbilityType.Unused, 5)
                .ModifyAbilityScore(AbilityType.Vitality, -4)
                .ModifyAbilityScore(AbilityType.Might, -4);
        }
    }
}
