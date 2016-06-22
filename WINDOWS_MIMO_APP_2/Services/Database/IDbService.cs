namespace WINDOWS_MIMO_APP_2.Services.Database
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface IDbService
    {
        void CreateDb();

        void addRecipeFavorite(Recipe recipe);

        void removeRecipeFavorite(int id);

        IList<RecipeFavorite> getFavorites();

        bool recipeFavoriteExists(string name);
    }
}
