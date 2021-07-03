using Xenomech.Enumeration;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Service.AbilityService
{
    /// <summary>
    /// Adds an FP requirement to activate a perk.
    /// </summary>
    public class PerkFPRequirement : IAbilityActivationRequirement
    {
        private readonly int _requiredFP;

        public PerkFPRequirement(int requiredFP)
        {
            _requiredFP = requiredFP;
        }

        public string CheckRequirements(uint player)
        {
            // DMs are assumed to be able to activate.
            if (GetIsDM(player)) return string.Empty;

            var fp = Stat.GetCurrentFP(player);

            if (fp >= _requiredFP) return string.Empty;
            return $"Not enough FP. (Required: {_requiredFP})";
        }

        public void AfterActivationAction(uint player)
        {
            if (GetIsDM(player)) return;

            Stat.ReduceFP(player, _requiredFP);
        }
    }
}
