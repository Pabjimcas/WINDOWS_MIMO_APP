namespace WINDOWS_MIMO_APP_2.Services.NavigationService
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Windows.UI.Xaml.Controls;
    public interface IRecipeService
    {
        Task<List<RecipeList>> GetRecipesAsync();
        Task<Recipe> GetRecipeAsync(int id);

    }
}
