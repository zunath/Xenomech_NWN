using System.Collections.Generic;

namespace Xenomech.Service.ImplantService
{
    public interface IImplantListDefinition
    {
        public Dictionary<string, ImplantDetail> BuildImplants();
    }
}
