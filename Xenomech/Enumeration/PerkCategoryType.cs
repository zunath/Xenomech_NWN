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
        TwoHandedGeneral = 5,

        [PerkCategory("Two Handed - Greatsword", true)]
        TwoHandedGreatsword = 6,

        [PerkCategory("Two Handed - Polearm", true)]
        TwoHandedPolearm = 7,

        [PerkCategory("Two Handed - Twin Blade", true)]
        TwoHandedTwinBlade = 8,
        
        [PerkCategory("Martial Arts - General", true)]
        MartialArtsGeneral = 10,

        [PerkCategory("Martial Arts - Knuckles", true)]
        MartialArtsKnuckles = 11,

        [PerkCategory("Martial Arts - Staff", true)]
        MartialArtsStaff = 12,

        [PerkCategory("Ranged - General", true)]
        RangedGeneral = 13,

        [PerkCategory("Ranged - Pistol", true)]
        RangedPistol = 14,

        [PerkCategory("Ranged - Throwing", true)]
        RangedThrowing = 15,

        [PerkCategory("Ranged - Rifle", true)]
        RangedRifle = 17,

        [PerkCategory("Ether", true)]
        Ether = 18,

        [PerkCategory("Armor - General", true)]
        ArmorGeneral = 19,

        [PerkCategory("Armor - Heavy", true)]
        ArmorHeavy = 20,

        [PerkCategory("Armor - Light", true)]
        ArmorLight = 21,

        [PerkCategory("Piloting", true)]
        Piloting = 22,

        [PerkCategory("First Aid", true)]
        FirstAid = 23,

        [PerkCategory("Smithery", true)]
        Smithery = 24,

        [PerkCategory("Cybertech", true)]
        Cybertech = 25,

        [PerkCategory("Fabrication", true)]
        Fabrication = 26,

        [PerkCategory("Gathering", true)]
        Gathering = 27
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
