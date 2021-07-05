using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Service.ItemService;
using static Xenomech.Core.NWScript.NWScript;
using Random = Xenomech.Service.Random;

namespace Xenomech.Feature.ItemDefinition
{
    public class ConsumableItemDefinition: IItemListDefinition
    {
        public Dictionary<string, ItemDetail> BuildItems()
        {
            var builder = new ItemBuilder();
            SlugShake(builder);

            return builder.Build();
        }

        private void SlugShake(ItemBuilder builder)
        {
            builder.Create("slug_shake")
                .Delay(1f)
                .PlaysAnimation(Animation.FireForgetDrink)
                .ReducesItemCharge()
                .ApplyAction((user, item, target, location) =>
                {
                    var ability = AbilityType.Invalid;
                    
                    switch (Random.D6(1))
                    {
                        case 1:
                            ability = AbilityType.Diplomacy;
                            break;
                        case 2:
                            ability = AbilityType.Vitality;
                            break;
                        case 3:
                            ability = AbilityType.Perception;
                            break;
                        case 4:
                            ability = AbilityType.Unused;
                            break;
                        case 5:
                            ability = AbilityType.Might;
                            break;
                        case 6:
                            ability = AbilityType.Spirit;
                            break;
                    }

                    var maxHP = GetMaxHitPoints(user);
                    ApplyEffectToObject(DurationType.Instant, EffectHeal(maxHP), user);
                    ApplyEffectToObject(DurationType.Temporary, EffectAbilityDecrease(ability, 50), user, 120f);

                });
        }
    }
}
