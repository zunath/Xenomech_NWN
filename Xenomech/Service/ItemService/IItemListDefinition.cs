using System.Collections.Generic;

namespace Xenomech.Service.ItemService
{
    public interface IItemListDefinition
    {
        public Dictionary<string, ItemDetail> BuildItems();
    }
}
