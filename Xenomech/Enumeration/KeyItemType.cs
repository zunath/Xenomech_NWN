using System;

namespace Xenomech.Enumeration
{
    public enum KeyItemType
    {
        [KeyItem(KeyItemCategoryType.Invalid, "Invalid", false, "")]
        Invalid = 0,
        [KeyItem(KeyItemCategoryType.Keys, "Taxi Hailing Device", true, "This device will enable you to call upon a taxi to quickly transport you across a region.")]
        TaxiHailingDevice = 1
    }

    public class KeyItemAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public KeyItemCategoryType Category { get; set; }
        public bool IsActive { get; set; }

        public KeyItemAttribute(KeyItemCategoryType category, string name, bool isActive, string description)
        {
            Category = category;
            Name = name;
            IsActive = isActive;
            Description = description;
        }
    }
}
