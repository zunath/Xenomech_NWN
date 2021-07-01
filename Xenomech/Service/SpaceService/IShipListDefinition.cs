using System.Collections.Generic;

namespace Xenomech.Service.SpaceService
{
    public interface IShipListDefinition
    {
        public Dictionary<string, ShipDetail> BuildShips();
    }
}
