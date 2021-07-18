using System;

namespace Xenomech.Service.MechService
{
    public enum MechFrameType
    {
        [MechFrame(1, 1, 0, 0, 0)]
        Invalid = 0,
        
        [MechFrame(100, 50, 1, 1, 1)]
        TestFrame = 1,
    }

    public class MechFrameAttribute : Attribute
    {
        public int HP { get; set; }
        public int Fuel { get; set; }
        public int HeadPartId { get; set; }
        public int TorsoPartId { get; set; }
        public int NeckPartId { get; set; }


        public MechFrameAttribute(int hp, int fuel, int headPartId, int torsoPartId, int neckPartId)
        {
            HP = hp;
            Fuel = fuel;
            HeadPartId = headPartId;
            TorsoPartId = torsoPartId;
            NeckPartId = neckPartId;
        }
    }
}
