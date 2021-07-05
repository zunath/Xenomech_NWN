using System;

namespace Xenomech.Enumeration
{
    public enum RecipeCategoryType
    {
        [RecipeCategory("Invalid", false)]
        Invalid = 0,
        [RecipeCategory("Uncategorized", true)]
        Uncategorized = 1,
        [RecipeCategory("Cloak", true)]
        Cloak = 2,
        [RecipeCategory("Belt", true)]
        Belt = 3,
        [RecipeCategory("Ring", true)]
        Ring = 4,
        [RecipeCategory("Necklace", true)]
        Necklace = 5,
        [RecipeCategory("Breastplate", true)]
        Breastplate = 6,
        [RecipeCategory("Helmet", true)]
        Helmet = 7,
        [RecipeCategory("Bracer", true)]
        Bracer = 8,
        [RecipeCategory("Legging", true)]
        Legging = 9,
        [RecipeCategory("Heavy Shield", true)]
        HeavyShield = 10,
        [RecipeCategory("Tunic", true)]
        Tunic = 11,
        [RecipeCategory("Cap", true)]
        Cap = 12,
        [RecipeCategory("Glove", true)]
        Glove = 13,
        [RecipeCategory("Boot", true)]
        Boot = 14,
        [RecipeCategory("Light Shield", true)]
        LightShield = 15,
        [RecipeCategory("Longsword", true)]
        Longsword = 16,
        [RecipeCategory("Knife", true)]
        Knife = 17,
        [RecipeCategory("Greatsword", true)]
        Greatsword = 19,
        [RecipeCategory("Polearm", true)]
        Polearm = 20,
        [RecipeCategory("Twin Blade", true)]
        TwinBlade = 21,     
        [RecipeCategory("Martial", true)]
        Martial = 23,
        [RecipeCategory("Baton", true)]
        Baton = 24,
        [RecipeCategory("Pistol", true)]
        Pistol = 25,
        [RecipeCategory("Throwing", true)]
        Throwing = 26,
        
        [RecipeCategory("Rifle", true)]
        Rifle = 28,
        
        [RecipeCategory("Furniture", true)]
        Furniture = 37,
        
        [RecipeCategory("Harvester", true)]
        Harvester = 45,
        [RecipeCategory("Stim Pack", true)]
        StimPack = 46,
        [RecipeCategory("Recovery", true)]
        Recovery = 47,
    }

    public class RecipeCategoryAttribute : Attribute
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public RecipeCategoryAttribute(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }
    }
}
