using System;

namespace Xenomech.Service.AbilityService
{
    // Note: Short names are what's displayed on the recast Gui element. They are limited to 14 characters.
    public enum RecastGroup
    {
        [RecastGroup("Invalid", "Invalid")]
        Invalid = 0,
        [RecastGroup("Anemo Bolt", "An. Bolt")]
        AnemoBolt = 1,
        [RecastGroup("Aqua Ice", "Aqua Bolt")]
        AquaIce = 2,
        [RecastGroup("Elemental Seal", "Elem. Seal")]
        ElementalSeal = 3,
        [RecastGroup("Elemental Spread", "Elem. Spread")]
        ElementalSpread = 4,
        [RecastGroup("Thermo Cube", "Therm. Cube")]
        ThermoCube = 5,
        [RecastGroup("Arcane Spikes", "Arc. Spikes")]
        ArcaneSpikes = 6,
        [RecastGroup("Arcane Spread", "Arc. Spread")]
        ArcaneSpread = 7,
        [RecastGroup("Convert", "Convert")]
        Convert = 8,
        [RecastGroup("Kaseii", "Kaseii")]
        Kaseii = 9,
        [RecastGroup("Ryokusho", "Ryokusho")]
        Ryokusho = 10,
        [RecastGroup("Sazanami", "Sazanami")]
        Sazanami = 11,
        [RecastGroup("Senkei", "Senkei")]
        Senkei = 12,
        [RecastGroup("Summon Spirit", "Summon Spirit")]
        SummonSpirit = 13,
        [RecastGroup("Hacking Blade", "Hacking Blade")]
        HackingBlade = 14,
        [RecastGroup("Riot Blade", "Riot Blade")]
        RiotBlade = 15,
        [RecastGroup("Poison Stab", "Poison Stab")]
        PoisonStab = 16,
        [RecastGroup("Backstab", "Backstab")]
        Backstab = 17,
        [RecastGroup("Ether Conduit", "E. Conduit")]
        EtherConduit = 18,
        [RecastGroup("Saber Strike", "Saber Strike")]
        SaberStrike = 19,
        [RecastGroup("Crescent Moon", "Cresc. Moon")]
        CrescentMoon = 20,
        [RecastGroup("Hard Slash", "Hard Slash")]
        HardSlash = 21,
        [RecastGroup("Skewer", "Skewer")]
        Skewer = 22,
        [RecastGroup("Double Thrust", "Double Thrust")]
        DoubleThrust = 23,
        [RecastGroup("Leg Sweep", "Leg Sweep")]
        LegSweep = 24,
        [RecastGroup("Cross Cut", "Cross Cut")]
        CrossCut = 25,
        [RecastGroup("Circle Slash", "Circle Slash")]
        CircleSlash = 26,
        [RecastGroup("Double Strike", "Double Strike")]
        DoubleStrike = 27,
        [RecastGroup("Electric Fist", "Electric Fist")]
        ElectricFist = 28,
        [RecastGroup("Striking Cobra", "Striking Cobra")]
        StrikingCobra = 29,
        [RecastGroup("Slam", "Slam")]
        Slam = 30,
        [RecastGroup("Spinning Whirl", "Spinning Whirl")]
        SpinningWhirl = 31,
        [RecastGroup("Quick Draw", "Quick Draw")]
        QuickDraw = 32,
        [RecastGroup("Double Shot", "Double Shot")]
        DoubleShot = 33,
        [RecastGroup("Explosive Toss", "Explosive Toss")]
        ExplosiveToss = 34,
        [RecastGroup("Piercing Toss", "Piercing Toss")]
        PiercingToss = 35,
        [RecastGroup("Full Auto", "Full Auto")]
        FullAuto = 36,
        [RecastGroup("Hammer Shot", "Hammer Shot")]
        HammerShot = 37,
        [RecastGroup("Tranquilizer Shot", "Tranq. Shot")]
        TranquilizerShot = 38,
        [RecastGroup("Crippling Shot", "Crippling Shot")]
        CripplingShot = 39,
        [RecastGroup("Grenades", "Grenades")]
        Grenades = 40,
        [RecastGroup("Rest", "Rest")]
        Rest = 41,
        [RecastGroup("Knockdown", "Knockdown")]
        Knockdown = 42,
        [RecastGroup("Parting Gift", "Parting Gift")]
        PartingGift = 43
    }

    public class RecastGroupAttribute: Attribute
    {
        public string Name { get; set; }
        public string ShortName { get; set; }

        public RecastGroupAttribute(string name, string shortName)
        {
            Name = name;
            ShortName = shortName;
        }
    }
}
