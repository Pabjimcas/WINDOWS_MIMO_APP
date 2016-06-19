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
        [Column("ID"), PrimaryKey(), AutoIncrement()]
        public int id { get; set; }

        [Column("Name")]
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
    {
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
        [Column("ID"), PrimaryKey(), AutoIncrement()]
        public int id { get; set; }

        [Column("Name")]
        public string name { get; set; }

        [Column("Description")]
        public string description { get; set; }

        [Column("Seconds")]
        public int seconds { get; set; }

        [Column("Photo")]
        public string photo { get; set; }

        [Column("recipe_id")]
        public int recipeId { get; set; }
    }
}
