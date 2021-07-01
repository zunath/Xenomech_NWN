using Xenomech.Core;
using Xenomech.Core.NWNX;
using Xenomech.Core.NWNX.Enum;

namespace Xenomech.Feature
{
    public static class FeedbackMessageConfiguration
    {
        /// <summary>
        /// When the module loads, configure the feedback messages.
        /// </summary>
        [NWNEventHandler("mod_load")]
        public static void ConfigureFeedbackMessages()
        {
            Feedback.SetFeedbackMessageHidden(FeedbackMessageTypes.UseitemCantUse, true);
            Feedback.SetFeedbackMessageHidden(FeedbackMessageTypes.CombatRunningOutOfAmmo, true);
        }
    }
}
