using System.Collections.Generic;
using Xenomech.Enumeration;
using Xenomech.Service.CraftService;

namespace Xenomech.Feature.RecipeDefinition.MedicineRecipeDefinition
{
    public class MedicineRecipes2 : IRecipeListDefinition
    {
        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            var builder = new RecipeBuilder();

            return builder.Build();
        }
    }
}