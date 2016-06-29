

namespace WINDOWS_MIMO_APP_2.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Task
    {
        public Task() { }
        public Task (TaskFavorite tf)
        {
            this.id = tf.id;
            this.name = tf.name;
            this.photo = tf.photo;
            this.seconds = tf.seconds;
            this.description = tf.description;
        }
        public static List<Task> convert(List<TaskFavorite> list)
        {
            List<Task> listTask = new List<Task>();
            foreach (TaskFavorite tf in list)
            {
                listTask.Add(new Task(tf));
            }
            return listTask;
        }
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? seconds { get; set; }
        public string photo { get; set; }
    }

    public class Ingredient
    {
        public Ingredient() { }
        public Ingredient(IngredientFavorite ingref)
        {
            this.id = ingref.id;
            this.name = ingref.name;
            this.photo = ingref.photo;
            this.baseType = ingref.baseType;
            this.category = ingref.category;
            this.frozen = ingref.frozen;
            
        }
        public static List<Ingredient> convert(List<IngredientFavorite> list)
        {
            List<Ingredient> listIngredient = new List<Ingredient>();
            foreach (IngredientFavorite ingref in list)
            {
                listIngredient.Add(new Ingredient(ingref));
            }
            return listIngredient;
        }
        public int id { get; set; }
        public string name { get; set; }
        public bool frozen { get; set; }
        public string category { get; set; }
        public string baseType { get; set; }
        public string photo { get; set; }
    }

    public class MeasureIngredient
    {
        public MeasureIngredient() { }
        public MeasureIngredient(MeasureIngredientFavorite mf, Ingredient i)
        {
            this.id = mf.id;
            this.ingredient = i;
            this.measure = mf.measure;
            this.quantity = mf.quantity;

        }
      
        public int id { get; set; }
        public string measure { get; set; }
        public double quantity { get; set; }
        public Ingredient ingredient { get; set; }
    }

    public class Recipe
    {
        public Recipe() { }
        public Recipe(RecipeFavorite rf)
        {
            this.id = rf.id;
            this.difficulty = rf.difficulty;
            this.author = rf.author;
            this.name = rf.name;
            this.photo = rf.photo;
            this.score = rf.score;
            this.portions = rf.portions;
        }
        
        public int id { get; set; }
        public List<Task> tasks { get; set; }
        public List<MeasureIngredient> measureIngredients { get; set; }
        public string name { get; set; }
        public int score { get; set; }
        public string author { get; set; }
        public int difficulty { get; set; }
        public string photo { get; set; }
        public int portions { get; set; }
    }
}
