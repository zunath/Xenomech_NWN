using System;

namespace Xenomech.Enumeration
{
    public enum SkillCategoryType
    {
        [SkillCategory("Invalid", false, 0)]
        Invalid = 0,
        [SkillCategory("Combat", true, 1)]
        Combat = 1,
        [SkillCategory("Ether", true, 2)]
        Ether = 2,
        [SkillCategory("Cybertech", true, 3)]
        Cybertech = 3,
        [SkillCategory("Crafting", true, 4)]
        Crafting = 4,
        [SkillCategory("Utility", true, 5)]
        Utility = 5,
        [SkillCategory("Languages", true, 6)]
        Languages = 6,
    }

    public class SkillCategoryAttribute : Attribute
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int Sequence { get; set; }

        public SkillCategoryAttribute(string name, bool isActive, int sequence)
        {
            Name = name;
            IsActive = isActive;
            Sequence = sequence;
        }
    }
}