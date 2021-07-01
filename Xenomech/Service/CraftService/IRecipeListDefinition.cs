using System.Collections.Generic;
using Xenomech.Enumeration;

namespace Xenomech.Service.CraftService
{
    public interface IRecipeListDefinition
    {
        public Dictionary<RecipeType, RecipeDetail> BuildRecipes();
    }
}
