﻿using System.Collections.Generic;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Service.SpawnService;

namespace Xenomech.Feature.SpawnDefinition
{
    public class CZ220SpawnDefinition: ISpawnListDefinition
    {
        public Dictionary<string, SpawnTable> BuildSpawnTables()
        {
            var builder = new SpawnTableBuilder();
            DroidSpawns(builder);
            MynockSpawns(builder);
            ColicoidExperimentSpawn(builder);

            return builder.Build();
        }

        private void DroidSpawns(SpawnTableBuilder builder)
        {
            builder.Create("CZ220_DROIDS", "CZ-220 Droids")
                .AddSpawn(ObjectType.Creature, "malsecdroid")
                .WithFrequency(50)
                .RandomlyWalks()

                .AddSpawn(ObjectType.Creature, "malspiderdroid")
                .WithFrequency(50)
                .RandomlyWalks();
        }

        private void MynockSpawns(SpawnTableBuilder builder)
        {
            builder.Create("CZ220_MYNOCKS", "CZ-220 Mynocks")
                .AddSpawn(ObjectType.Creature, "mynock")
                .WithFrequency(100)
                .RandomlyWalks();
        }

        private void ColicoidExperimentSpawn(SpawnTableBuilder builder)
        {
            builder.Create("CZ220_COLICOID_EXPERIMENT", "Colicoid Experiment")
                .AddSpawn(ObjectType.Creature, "colicoidexp")
                .WithFrequency(100)
                .RandomlyWalks();
        }
    }
}
