using System.Collections.Generic;
using Xenomech.Core.NWNX.Enum;

namespace Xenomech.Entity
{
    public class PlayerVisibilityObject: EntityBase
    {
        public PlayerVisibilityObject()
        {
            ObjectVisibilities = new Dictionary<string, VisibilityType>();
        }
        public override string KeyPrefix => "PlayerVisibilityObject";
        public Dictionary<string, VisibilityType> ObjectVisibilities { get; set; }
    }
}
