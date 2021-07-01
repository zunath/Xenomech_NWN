using System.Collections.Generic;

namespace Xenomech.Service.SpaceService
{
    public interface IShipModuleListDefinition
    {
        public Dictionary<string, ShipModuleDetail> BuildShipModules();
    }
}
