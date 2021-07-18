using System;

namespace Xenomech.Service.MechService
{
    public enum MechLeftArmType
    {
        [MechLeftArm(1, 0, 0, 0)]
        Invalid = 0,

        [MechLeftArm(40, 1, 1, 1)]
        TestLeftArm = 1,
    }

    public class MechLeftArmAttribute : Attribute
    {
        public int HP { get; set; }
        public int BicepPartId { get; set; }
        public int ForearmPartId { get; set; }
        public int HandPartId { get; set; }

        public MechLeftArmAttribute(int hp, int bicepPartId, int forearmPartId, int handPartId)
        {
            HP = hp;
            BicepPartId = bicepPartId;
            ForearmPartId = forearmPartId;
            HandPartId = handPartId;
        }
    }
}
