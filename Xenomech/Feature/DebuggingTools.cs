using System;
using Xenomech.Core;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Native;
using Xenomech.Service;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature
{
    public static class DebuggingTools
    {
        [NWNEventHandler("test")]
        public static void Test()
        {
            var player = GetLastUsedBy();

            SetPhenoType(PhenoType.TigerFang, player);
        }
    }
}
