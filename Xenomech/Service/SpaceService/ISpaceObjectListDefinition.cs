using System.Collections.Generic;

namespace Xenomech.Service.SpaceService
{
    public interface ISpaceObjectListDefinition
    {
        public Dictionary<string, SpaceObjectDetail> BuildSpaceObjects();
    }
}
