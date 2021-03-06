using Xenomech.Core;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Service
{
    public static class Activity
    {
        public static void SetBusy(uint target)
        {
            SetLocalBool(target, "IS_BUSY", true);
        }

        public static bool IsBusy(uint target)
        {
            return GetLocalBool(target, "IS_BUSY");
        }

        public static void ClearBusy(uint target)
        {
            DeleteLocalBool(target, "IS_BUSY");
        }

        /// <summary>
        /// When a player enters the module, wipe their temporary "busy" status.
        /// </summary>
        [NWNEventHandler("mod_enter")]
        public static void WipeStatusOnEntry()
        {
            var player = GetEnteringObject();
            ClearBusy(player);
        }
    }
}
