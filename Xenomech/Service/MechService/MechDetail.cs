namespace Xenomech.Service.MechService
{
    public class MechDetail
    {
        public int MaxFrameHP { get; set; }
        public int MaxLeftArmHP { get; set; }
        public int MaxRightArmHP { get; set; }
        public int MaxLegsHP { get; set; }
        public int MaxFuel { get; set; }

        public int CurrentFrameHP { get; set; }
        public int CurrentLeftArmHP { get; set; }
        public int CurrentRightArmHP { get; set; }
        public int CurrentLegsHP { get; set; }
        public int CurrentFuel { get; set; }

    }
}
