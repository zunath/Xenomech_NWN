﻿using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Service.ImplantService;

namespace Xenomech.Feature.ImplantDefinition
{
    public class IntuitionImplantDefinition: IImplantListDefinition
    {
        private readonly ImplantBuilder _builder = new ImplantBuilder();

        public Dictionary<string, ImplantDetail> BuildImplants()
        {
            IntuitionImplant1();
            IntuitionImplant2();
            IntuitionImplant3();
            IntuitionImplant4();
            IntuitionImplant5();

            return _builder.Build();
        }

        private void IntuitionImplant1()
        {
            _builder.Create("h_imp_inu1")
                .Name("Intuition")
                .Description("+1 WIS, -1 CON")
                .RequiredLevel(1)
                .Slot(ImplantSlotType.Head)
                .ModifyAbilityScore(AbilityType.Spirit, 1)
                .ModifyAbilityScore(AbilityType.Vitality, -1);
        }

        private void IntuitionImplant2()
        {
            _builder.Create("h_imp_inu2")
                .Name("Intuition")
                .Description("+2 WIS, -1 CON, -1 STR")
                .RequiredLevel(2)
                .Slot(ImplantSlotType.Head)
                .ModifyAbilityScore(AbilityType.Spirit, 2)
                .ModifyAbilityScore(AbilityType.Vitality, -1)
                .ModifyAbilityScore(AbilityType.Might, -1);
        }
        private void IntuitionImplant3()
        {
            _builder.Create("h_imp_inu3")
                .Name("Intuition")
                .Description("+3 WIS, -2 CON, -2 STR")
                .RequiredLevel(3)
                .Slot(ImplantSlotType.Head)
                .ModifyAbilityScore(AbilityType.Spirit, 3)
                .ModifyAbilityScore(AbilityType.Vitality, -2)
                .ModifyAbilityScore(AbilityType.Might, -2);
        }
        private void IntuitionImplant4()
        {
            _builder.Create("h_imp_inu4")
                .Name("Intuition")
                .Description("+4 WIS, -3 CON, -3 STR")
                .RequiredLevel(4)
                .Slot(ImplantSlotType.Head)
                .ModifyAbilityScore(AbilityType.Spirit, 4)
                .ModifyAbilityScore(AbilityType.Vitality, -3)
                .ModifyAbilityScore(AbilityType.Might, -3);
        }
        private void IntuitionImplant5()
        {
            _builder.Create("h_imp_inu5")
                .Name("Intuition")
                .Description("+5 WIS, -4 CON, -4 STR")
                .RequiredLevel(5)
                .Slot(ImplantSlotType.Head)
                .ModifyAbilityScore(AbilityType.Spirit, 5)
                .ModifyAbilityScore(AbilityType.Vitality, -4)
                .ModifyAbilityScore(AbilityType.Might, -4);
        }
    }
}
