using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using WINDOWS_MIMO_APP_2.Models;

namespace WINDOWS_MIMO_APP_2.Services.Database
{
    class DbService : IDbService
    {
        private static readonly string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "offline.sqlite");

        public void CreateDb()
        {
            using (var cnx = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath))
            {
                cnx.CreateTable<RecipeFavorite>();
                //cnx.CreateTable<MeasureIngredientFavorite>();
                //cnx.CreateTable<IngredientFavorite>();
                //cnx.CreateTable<TaskFavorite>();
                cnx.Commit();
            }
        }

        public void addRecipeFavorite(Recipe recipe)
        {
            using (var cnx = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath))
            {
                RecipeFavorite rFavorite = new RecipeFavorite();
                rFavorite.name = recipe.name;
                rFavorite.photo = recipe.photo;
                rFavorite.portions = recipe.portions;
                rFavorite.score = recipe.score;
                rFavorite.author = recipe.author;
                rFavorite.difficulty = recipe.difficulty;

                cnx.Insert(rFavorite);

                cnx.Commit();
            }
        }

        public IList<RecipeFavorite> getFavorites()
        {
            using (var cnx = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath))
            {
                try
                {
                    return cnx.Table<RecipeFavorite>()
                        .ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public void removeRecipeFavorite(int id)
        {
            using (var cnx = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath))
            {
                var recipe = cnx.Query<RecipeFavorite>("Select * from Recipe where id = " + id).FirstOrDefault();

                cnx.Delete(recipe);
            }
        }
    }
}
