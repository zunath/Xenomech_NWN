using System.Collections.Generic;
using Xenomech.Enumeration;
using Xenomech.Service.CraftService;

namespace Xenomech.Feature.RecipeDefinition.EngineeringRecipeDefinition
{
    public class EngineeringRecipes4 : IRecipeListDefinition
    {
        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            var builder = new RecipeBuilder();

            return builder.Build();
        }
    }
}