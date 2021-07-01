﻿using System.Collections.Generic;
using Xenomech.Service.SpawnService;

namespace Xenomech.Feature.SpawnDefinition
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
