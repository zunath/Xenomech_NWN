using System.Collections.Generic;
using Xenomech.Service.SpaceService;

namespace Xenomech.Feature.SpaceObjectDefinition
{
    public class AsteroidSpaceObjectDefinition : ISpaceObjectListDefinition
    {
        private readonly SpaceObjectBuilder _builder = new SpaceObjectBuilder();

        public Dictionary<string, SpaceObjectDetail> BuildSpaceObjects()
        {
            DemoEnemy();

            return _builder.Build();
        }

        private void DemoEnemy()
        {
            _builder.Create("spc_asteroid")
                .ItemTag("asteroid");
        }

    }
}