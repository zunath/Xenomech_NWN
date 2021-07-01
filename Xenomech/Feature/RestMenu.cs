using Xenomech.Core;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Feature.DialogDefinition;
using Xenomech.Service;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature
{
    public static class RestMenu
    {
        [NWNEventHandler("mod_rest")]
        public static void OpenRestMenu()
        {
            var player = GetLastPCRested();
            var restType = GetLastRestEventType();

            if (restType != RestEventType.Started ||
                !GetIsObjectValid(player) ||
                GetIsDM(player)) return;

            AssignCommand(player, () => ClearAllActions());

            Dialog.StartConversation(player, player, nameof(RestMenuDialog));
        }
    }
}
