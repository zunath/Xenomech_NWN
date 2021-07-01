using System.Collections.Generic;
using Xenomech.Service.QuestService;

namespace Xenomech.Feature.QuestDefinition
{
    public class MonCalaQuestDefinition : IQuestListDefinition
    {
        public Dictionary<string, QuestDetail> BuildQuests()
        {
            var builder = new QuestBuilder();

            return builder.Build();
        }
    }
}