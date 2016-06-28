namespace WINDOWS_MIMO_APP_2.Models
{
    using SQLite.Net.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    [Table("Recipe")]
    public class RecipeFavorite
    {
        public RecipeFavorite()
        {

        }
        public RecipeFavorite(Recipe recipe)
        {
            this.author = recipe.author;
            this.difficulty = recipe.difficulty;
            this.name = recipe.name;
            this.photo = recipe.photo;
            this.portions = recipe.portions;
            this.score = recipe.score;
            
        }
        [Column("ID"), PrimaryKey(), AutoIncrement()]
        public int id { get; set; }

        [Column("Name"),Unique]
        public string name { get; set; }

        [Column("Score")]
        public int score { get; set; }

        [Column("Author")]
        public string author { get; set; }

        [Column("Difficulty")]
        public int difficulty { get; set; }

        [Column("Photo")]
        public string photo { get; set; }

        [Column("Portions")]
        public int portions { get; set; }
    }

    [Table("Ingredient")]
    public class IngredientFavorite
    {   public IngredientFavorite() { }
        public IngredientFavorite (Ingredient ingredient)
        {
            this.baseType = ingredient.baseType;
            this.category = ingredient.category;
            this.frozen = ingredient.frozen;
            this.name = ingredient.name;
            this.photo = ingredient.photo;
        }
        [Column("ID"), PrimaryKey(), AutoIncrement()]
        public int id { get; set; }

        [Column("Name")]
        public string name { get; set; }

        [Column("Frozen")]
        public bool frozen { get; set; }

        [Column("Category")]
        public string category { get; set; }

        [Column("BaseType")]
        public string baseType { get; set; }

        [Column("Photo")]
        public string photo { get; set; }

    }

    [Table("MeasureIngredient")]
    public class MeasureIngredientFavorite
    {
        public MeasureIngredientFavorite() { }
        public MeasureIngredientFavorite(MeasureIngredient measureIngredient,int ingredientId,int recipeId)
        {
            this.ingredientId = ingredientId;
            this.recipeId = recipeId;
            this.measure = measureIngredient.measure;
            this.quantity = measureIngredient.quantity;
        }
        [Column("ID"), PrimaryKey(), AutoIncrement()]
        public int id { get; set; }

        [Column("Measure")]
        public string measure { get; set; }

        [Column("Quantity")]
        public double quantity { get; set; }

        [Column("indredient_id")]
        public int ingredientId { get; set; }

        [Column("recipe_id")]
        public int recipeId { get; set; }
    }

    [Table("Task")]
    public class TaskFavorite
    {
        public TaskFavorite() { }
        public TaskFavorite (Task task,int recipeId)
        {
            this.name = task.name;
            this.photo = task.photo;
            this.recipeId = recipeId;
            this.seconds = task.seconds;
            this.description = task.description;
        }
        [Column("ID"), PrimaryKey(), AutoIncrement()]
        public int id { get; set; }

        [Column("Name")]
        public string name { get; set; }

        [Column("Description")]
        public string description { get; set; }

        [Column("Seconds")]
        public int? seconds { get; set; }

        [Column("Photo")]
        public string photo { get; set; }

        [Column("recipe_id")]
        public int recipeId { get; set; }
    }
}
