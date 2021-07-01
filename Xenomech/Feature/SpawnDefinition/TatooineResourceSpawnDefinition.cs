﻿using System.Collections.Generic;
using SWLOR.Game.Server.Service.SpawnService;

namespace SWLOR.Game.Server.Feature.SpawnDefinition
{
    public class TatooineResourceSpawnDefinition: ISpawnListDefinition
    {
        public Dictionary<string, SpawnTable> BuildSpawnTables()
        {
            var builder = new SpawnTableBuilder();


            return builder.Build();
        }
    }
}
