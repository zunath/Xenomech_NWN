using Xenomech.Entity;
using Xenomech.Enumeration;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Service.PerkService
{
    public class PerkUnlockRequirement: IPerkRequirement
    {
        private readonly PerkType _perkType;

        public PerkUnlockRequirement(PerkType perkType)
        {
            _perkType = perkType;
        }

        public string CheckRequirements(uint player)
        {
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);
            return !dbPlayer.UnlockedPerks.ContainsKey(_perkType) 
                ? "Perk has not been unlocked yet." 
                : string.Empty;
        }

        public string RequirementText => "Perk must be unlocked.";
    }
}
