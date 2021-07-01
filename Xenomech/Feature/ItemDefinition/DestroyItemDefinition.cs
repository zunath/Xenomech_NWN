using System.Collections.Generic;
using Xenomech.Feature.DialogDefinition;
using Xenomech.Service.ItemService;
using static Xenomech.Core.NWScript.NWScript;
using Dialog = Xenomech.Service.Dialog;

namespace Xenomech.Feature.ItemDefinition
{
    public class DestroyItemDefinition: IItemListDefinition
    {
        private readonly ItemBuilder _builder = new ItemBuilder();

        public Dictionary<string, ItemDetail> BuildItems()
        {
            _builder.Create("player_guide", "survival_knife")
                .ApplyAction((user, item, target, location) =>
                {
                    SetLocalObject(user, "DESTROY_ITEM", item);
                    Dialog.StartConversation(user, user, nameof(DestroyItemDialog));
                });

            return _builder.Build();
        }
    }
}
