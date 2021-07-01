using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Service.SpawnService;

namespace Xenomech.Feature.SpawnDefinition
{
    public class ViscaraResourceSpawnDefinition: ISpawnListDefinition
    {
        public Dictionary<string, SpawnTable> BuildSpawnTables()
        {
            var builder = new SpawnTableBuilder();
            CoxxionBase(builder);
            WildlandsResources(builder);

            return builder.Build();
        }

        private void CoxxionBase(SpawnTableBuilder builder)
        {
            builder.Create("COXXION_BASE_SCAVENGE")
                .AddSpawn(ObjectType.Placeable, "v_cox_scav");
        }

        private void WildlandsResources(SpawnTableBuilder builder)
        {
            builder.Create("RESOURCES_VISCARA_WILDLANDS")
                .AddSpawn(ObjectType.Placeable, "veldite_vein");
        }
    }
}
