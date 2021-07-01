using Xenomech.Core;
using Xenomech.Service;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature
{
    public static class DebuggingTools
    {
        [NWNEventHandler("test2")]
        public static void KillMe()
        {
            var player = GetLastUsedBy();
            
            Space.ApplyShipDamage(player, player, 999);
        }

    }
}
