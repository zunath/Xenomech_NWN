using Xenomech.Enumeration;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Service.AbilityService
{
    public delegate int PerkEPRequirementDelegate(uint player);

    /// <summary>
    /// Adds an EP requirement to activate a perk.
    /// </summary>
    public class PerkEPRequirement : IAbilityActivationRequirement
    {
        private readonly PerkEPRequirementDelegate _requiredEPDelegate;

        public PerkEPRequirement(PerkEPRequirementDelegate requiredEPDelegate)
        {
            _requiredEPDelegate = requiredEPDelegate;
        }

        public string CheckRequirements(uint player)
        {
            // DMs are assumed to be able to activate.
            if (GetIsDM(player)) return string.Empty;

            var ep = Stat.GetCurrentEP(player);
            var requiredEP = _requiredEPDelegate(player);

            if (ep >= requiredEP) return string.Empty;
            return $"Not enough EP. (Required: {_requiredEPDelegate})";
        }

        public void AfterActivationAction(uint player)
        {
            if (GetIsDM(player)) return;
            var requiredEP = _requiredEPDelegate(player);

            Stat.ReduceEP(player, requiredEP);
        }
    }
}
