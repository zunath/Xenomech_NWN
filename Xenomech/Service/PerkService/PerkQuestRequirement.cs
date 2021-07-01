﻿using Xenomech.Entity;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Service.PerkService
{
    public class PerkQuestRequirement : IPerkRequirement
    {
        private readonly string _questId;

        public PerkQuestRequirement(string questId)
        {
            _questId = questId;
        }

        public string CheckRequirements(uint player)
        {
            var quest = Quest.GetQuestById(_questId);
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);
            var error = $"You have not completed the quest '{quest.Name}'.";

            if (!dbPlayer.Quests.ContainsKey(_questId)) return error;

            var playerQuest = dbPlayer.Quests[_questId];
            if (playerQuest.TimesCompleted <= 0) return error;

            return string.Empty;
        }

        public string RequirementText
        {
            get
            {
                var quest = Quest.GetQuestById(_questId);
                return $"Quest: {quest.Name} Completed";
            }
        }
    }
}
