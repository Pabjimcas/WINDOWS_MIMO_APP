namespace WINDOWS_MIMO_APP_2.Services.NavigationService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Views;
    using Windows.UI.Xaml.Controls;
    using Models;
    using System.Net.Http;
    using Newtonsoft.Json;
    public class RecipeService : IRecipeService
    {
        public async Task<List<RecipeList>> GetRecipesAsync()
        {
            string url = $"http://otakucook.herokuapp.com/recipes";
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync(url);

            if (!string.IsNullOrWhiteSpace(result))
            {
                var objectProcessed = JsonConvert.DeserializeObject<List<RecipeList>>(result);
                return objectProcessed;
            }

            return null;
        }
    }
}
