using System.Collections.Generic;
using Xenomech.Enumeration;
using Xenomech.Service.CraftService;

namespace Xenomech.Feature.RecipeDefinition.FabricationRecipeDefinition
{
    public class FabricationRecipes1 : IRecipeListDefinition
    {
        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            var builder = new RecipeBuilder();

            return builder.Build();
        }
    }
}