using System;

namespace Xenomech.Enumeration
{
    public enum SkillCategoryType
    {
        [SkillCategory("Invalid", false, 0)]
        Invalid = 0,
        [SkillCategory("Combat", true, 1)]
        Combat = 1,
        [SkillCategory("Ether", true, 1)]
        Ether = 2,
        [SkillCategory("Crafting", true, 4)]
        Crafting = 3,
        [SkillCategory("Utility", true, 6)]
        Utility = 4,
        [SkillCategory("Languages", true, 8)]
        Languages = 5,
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