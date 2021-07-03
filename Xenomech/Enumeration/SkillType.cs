using System;

namespace Xenomech.Enumeration
{
    public enum SkillType
    {
        [Skill(SkillCategoryType.Invalid, 
            "Invalid", 
            0, 
            false, 
            "Unused in-game.", 
            false)]
        Invalid = 0,

        // Melee Combat
        [Skill(SkillCategoryType.Combat, 
            "One-Handed", 
            50, 
            true, 
            "Ability to use one-handed weapons like longswords and knives.", 
            true)]
        OneHanded = 1,

        [Skill(SkillCategoryType.Combat, 
            "Two-Handed", 
            50, 
            true, 
            "Ability to use heavy weapons like greatswords, polearms, and twin blades in combat.", 
            true)]
        TwoHanded = 2,

        [Skill(SkillCategoryType.Combat, 
            "Martial Arts", 50, 
            true, 
            "Ability to fight using knuckles and staves in combat.", 
            true)]
        MartialArts = 3,

        [Skill(SkillCategoryType.Combat, 
            "Ranged", 
            50, 
            true, 
            "Ability to use ranged weapons like pistols, cannons, and rifles in combat.", 
            true)]
        Ranged = 4,

        [Skill(SkillCategoryType.Combat,
            "Force",
            50,
            true,
            "Ability to use Ether abilities. Only available to 'Mage' character types.",
            true)]
        Force = 5,

        [Skill(SkillCategoryType.Combat, 
            "Armor", 
            50, 
            true,
            "Ability to effectively wear and defend against attacks with armor.", 
            true)]
        Armor = 6,

        // Utility
        [Skill(SkillCategoryType.Utility,
            "Piloting",
            50,
            true,
            "Ability to pilot mechs.",
            true)]
        Piloting = 7,

        [Skill(SkillCategoryType.Utility,
            "First Aid",
            50,
            true,
            "Ability to treat bodily injuries in the field with healing kits and stim packs.",
            true)]
        FirstAid = 8,

        // Crafting
        [Skill(SkillCategoryType.Crafting, 
            "Smithery", 
            50, 
            true, 
            "Ability to create weapons and armor like vibroblades, blasters, and helmets.", 
            true)]
        Smithery = 9,
        
        [Skill(SkillCategoryType.Crafting,
            "Cybertech", 
            50,
            true,
            "Ability to create and install implants.",
            true)]
        Cybertech = 11,

        [Skill(SkillCategoryType.Crafting, 
            "Fabrication", 
            50, 
            true, 
            "Ability to create base structures, furniture, and starships.", 
            true)]
        Fabrication = 12,

        [Skill(SkillCategoryType.Crafting, 
            "Gathering", 
            50, 
            true, 
            "Ability to harvest raw materials and scavenge for supplies.", 
            true)]
        Gathering = 13,

        [Skill(SkillCategoryType.Languages, 
            "Basic", 
            20, 
            true, 
            "Ability to speak the Basic language.", 
            false)]
        Basic = 30
    }

    public class SkillAttribute : Attribute
    {
        public SkillCategoryType Category { get; set; }
        public string Name { get; set; }
        public int MaxRank { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public bool ContributesToSkillCap { get; set; }

        public SkillAttribute(
            SkillCategoryType category,
            string name,
            int maxRank,
            bool isActive,
            string description,
            bool contributesToSkillCap)
        {
            Category = category;
            Name = name;
            MaxRank = maxRank;
            IsActive = isActive;
            Description = description;
            ContributesToSkillCap = contributesToSkillCap;
        }
    }
}
