using System;

namespace Xenomech.Service.MechService
{
    public enum MechLegType
    {
        [MechLeg(1, 0, 0, 0)]
        Invalid = 0,

        [MechLeg(40, 1, 1, 1)]
        TestLegs = 1,
    }

    public class MechLegAttribute : Attribute
    {
        public int HP { get; set; }
        public int ThighPartId { get; set; }
        public int ShinPartId { get; set; }
        public int FootPartId { get; set; }

        public MechLegAttribute(int hp, int thighPartId, int shinPartId, int footPartId)
        {
            HP = hp;
            ThighPartId = thighPartId;
            ShinPartId = shinPartId;
            FootPartId = footPartId;
        }
    }
}
