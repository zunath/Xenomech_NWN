using System;

namespace Xenomech.Core.NWScript.Enum
{
    [Flags]
    public enum EffectDurationType
    {
        Instant = 0,
        Temporary = 1,
        Permanent = 2,
        Equipped = 3,
        Innate = 4,
        Mask = 0x7
    }
}