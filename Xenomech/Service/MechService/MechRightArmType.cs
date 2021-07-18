using System;

namespace Xenomech.Service.MechService
{
    public enum MechRightArmType
    {
        [MechRightArm(1, 0,  0, 0)]
        Invalid = 0,

        [MechRightArm(40, 1, 1, 1)]
        TestRightArm = 1,
    }

    public class MechRightArmAttribute : Attribute
    {
        public int HP { get; set; }
        public int BicepPartId { get; set; }
        public int ForearmPartId { get; set; }
        public int HandPartId { get; set; }

        public MechRightArmAttribute(int hp, int bicepPartId, int forearmPartId, int handPartId)
        {
            HP = hp;
            BicepPartId = bicepPartId;
            ForearmPartId = forearmPartId;
            HandPartId = handPartId;
        }
    }
}
