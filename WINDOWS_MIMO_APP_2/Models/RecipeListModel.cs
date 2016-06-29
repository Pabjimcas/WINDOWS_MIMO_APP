

using System.Collections.Generic;

namespace WINDOWS_MIMO_APP_2.Models
{
    
    public class RecipeList
    {
        public int id { get; set; }
        public string name { get; set; }
        public RecipeList(RecipeFavorite recipe)
        {
            this.id = recipe.id;
            this.name = recipe.name;
        }
        public RecipeList() { }
        public static List<RecipeList> convert(List<RecipeFavorite> list)
        {
            List<RecipeList> recipeList = new List<RecipeList>();
            foreach (RecipeFavorite recipe in list)
            {
                recipeList.Add(new RecipeList(recipe));
            }
            return recipeList;
        }

    }
}
