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
                cnx.CreateTable<MeasureIngredientFavorite>();
                cnx.CreateTable<IngredientFavorite>();
                cnx.CreateTable<TaskFavorite>();
                cnx.Commit();
            }
        }

        public void addRecipeFavorite(Recipe recipe)
        {
            
                RecipeFavorite rFavorite = new RecipeFavorite(recipe);

                int Rid = insertUpdateRecipe(rFavorite);
          
                foreach (Models.Task t in recipe.tasks)
                {
                    TaskFavorite tFavorite = new TaskFavorite(t, Rid);
                    insertUpdateTask(tFavorite);
                }

                foreach (Models.MeasureIngredient m in recipe.measureIngredients)
                {
                    IngredientFavorite iFavorite = new IngredientFavorite(m.ingredient);
                    int Iid = insertUpdateIngredient(iFavorite);
                    MeasureIngredientFavorite mFavorite = new MeasureIngredientFavorite(m, Iid, Rid);
                    insertUpdateMeasure(mFavorite);
                }
            using (var cnx = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath))
            {
                cnx.Commit();

            }
        }
        public static int insertUpdateIngredient(IngredientFavorite ingref)
        {
            using (var cnx = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath))
            {
                try
                {
                    int inserted = cnx.Insert(ingref); //will be 1 if successful
                    if (inserted > 0)
                        return ingref.id; //Acording to the documentation for the SQLIte component, the Insert method updates the id by reference
                    return inserted;
                }
                catch (SQLiteException ex)
                {
                    return -1;
                }
            }
        }
        public static int insertUpdateTask(TaskFavorite tf)
        {
            using (var cnx = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath))
            {
                try
                {
                    int inserted = cnx.Insert(tf); //will be 1 if successful
                    if (inserted > 0)
                        return tf.id; //Acording to the documentation for the SQLIte component, the Insert method updates the id by reference
                    return inserted;
                }
                catch (SQLiteException ex)
                {
                    return -1;
                }
            }
        }
        public static int insertUpdateRecipe(RecipeFavorite rf)
        {
            using (var cnx = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath))
            {
                try
                {
                    int inserted = cnx.Insert(rf); //will be 1 if successful
                    if (inserted > 0)
                        return rf.id; //Acording to the documentation for the SQLIte component, the Insert method updates the id by reference
                    return inserted;
                }
                catch (SQLiteException ex)
                {
                    return -1;
                }
            }
        }
        public static int insertUpdateMeasure(MeasureIngredientFavorite mf)
        {
            using (var cnx = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath))
            {
                try
                {
                    int inserted = cnx.Insert(mf); //will be 1 if successful
                    if (inserted > 0)
                        return mf.id; //Acording to the documentation for the SQLIte component, the Insert method updates the id by reference
                    return inserted;
                }
                catch (SQLiteException ex)
                {
                    return -1;
                }
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

        public Recipe getFavoriteRecipe(string name)
        {
            RecipeFavorite recipeFavorite = getRecipeFavoriteObject(name);
            List<TaskFavorite> taskList = getTaskListFavorite(recipeFavorite.id);
            List<MeasureIngredientFavorite> measureList = getMeasureListFavorite(recipeFavorite.id);
            List<IngredientFavorite> ingredientList = new List<IngredientFavorite>();
            foreach (MeasureIngredientFavorite m in measureList)
            {
                ingredientList.Add(getIngredientListFavorite(m.ingredientId));
            }
            Recipe recipe = new Recipe(recipeFavorite);
            List<Models.Task> listT = Models.Task.convert(taskList);
            recipe.tasks = listT;
            List<Models.Ingredient> listI = Models.Ingredient.convert(ingredientList);
            List<Models.MeasureIngredient> listM = new List<MeasureIngredient>();
            foreach (MeasureIngredientFavorite mf in measureList)
            {

                Ingredient ingredient = (from i in listI where i.id == mf.ingredientId select i).First();
               MeasureIngredient measure = new  MeasureIngredient(mf,ingredient);
                listM.Add(measure);
            }
            recipe.measureIngredients = listM;
            return recipe;
        }
        public IngredientFavorite getIngredientListFavorite(int id)
        {
            using (var cnx = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath))
            {
                IngredientFavorite ingredient = cnx.Query<IngredientFavorite>("Select * from Ingredient where ID = " + id).SingleOrDefault();
                return ingredient;

            }
        }
        public List<MeasureIngredientFavorite> getMeasureListFavorite(int id)
        {
            using (var cnx = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath))
            {
                List<MeasureIngredientFavorite> list = cnx.Query<MeasureIngredientFavorite>("Select * from MeasureIngredient where recipe_id = " + id);
                return list;

            }
        }
        public List<TaskFavorite> getTaskListFavorite(int id)
        {
            using (var cnx = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath))
            {
                List<TaskFavorite> list = cnx.Query<TaskFavorite>("Select * from Task where recipe_id = " + id);
                return list;

            }
        }
        public RecipeFavorite getRecipeFavoriteObject(string name)
        {
            using (var cnx = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath))
            {
                var recipeFav = cnx.Query<RecipeFavorite>("Select * from Recipe where name = '" + name + "'").FirstOrDefault();
                return recipeFav;
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

        public bool recipeFavoriteExists(string name)
        {
            using (var cnx = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath))
            {
                try
                {
                    var results = cnx.Query<RecipeFavorite>("Select * from Recipe where name = '" + name + "'");
                    if (results.Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    return false;
                    throw ex;
                }

            }
        }
    }
}
