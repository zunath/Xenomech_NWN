using System;

namespace Xenomech.Enumeration
{
    public enum PerkCategoryType
    {
        [PerkCategory("Invalid", false)]
        Invalid = 0,

        [PerkCategory("One Handed - General", true)]
        OneHandedGeneral = 1,

        [PerkCategory("One Handed - Longsword", true)]
        OneHandedLongsword = 2,

        [PerkCategory("One Handed - Knife", true)]
        OneHandedKnife = 3,

        [PerkCategory("Two Handed - General", true)]
        TwoHandedGeneral = 4,

        [PerkCategory("Two Handed - Greatsword", true)]
        TwoHandedGreatsword = 5,

        [PerkCategory("Two Handed - Polearm", true)]
        TwoHandedPolearm = 6,

        [PerkCategory("Two Handed - Twin Blade", true)]
        TwoHandedTwinBlade = 7,
        
        [PerkCategory("Martial Arts - General", true)]
        MartialArtsGeneral = 8,

        [PerkCategory("Martial Arts - Knuckles", true)]
        MartialArtsKnuckles = 9,

        [PerkCategory("Martial Arts - Staff", true)]
        MartialArtsStaff = 10,

        [PerkCategory("Ranged - General", true)]
        RangedGeneral = 11,

        [PerkCategory("Ranged - Pistol", true)]
        RangedPistol = 12,

        [PerkCategory("Ranged - Throwing", true)]
        RangedThrowing = 13,

        [PerkCategory("Ranged - Rifle", true)]
        RangedRifle = 14,

        [PerkCategory("Ether - Elemental", true)]
        EtherElemental = 15,

        [PerkCategory("Ether - Arcane", true)]
        EtherArcane = 16,

        [PerkCategory("Ether - Spiritbond", true)]
        EtherSpiritbond = 17,

        [PerkCategory("Armor - General", true)]
        ArmorGeneral = 18,

        [PerkCategory("Armor - Heavy", true)]
        ArmorHeavy = 19,

        [PerkCategory("Armor - Light", true)]
        ArmorLight = 20,

        [PerkCategory("Piloting", true)]
        Piloting = 21,

        [PerkCategory("First Aid", true)]
        FirstAid = 22,

        [PerkCategory("Smithery", true)]
        Smithery = 23,

        [PerkCategory("Cybertech", true)]
        Cybertech = 24,

        [PerkCategory("Fabrication", true)]
        Fabrication = 25,

        [PerkCategory("Gathering", true)]
        Gathering = 26
    }

    public class PerkCategoryAttribute : Attribute
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public PerkCategoryAttribute(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }
    }
}
