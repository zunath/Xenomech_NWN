using System.Collections.Generic;

namespace Xenomech.Service.QuestService
{
    public interface IQuestListDefinition
    {
        public Dictionary<string, QuestDetail> BuildQuests();
    }
}
