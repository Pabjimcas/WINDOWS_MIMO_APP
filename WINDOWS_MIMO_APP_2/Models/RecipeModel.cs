

namespace WINDOWS_MIMO_APP_2.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Task
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? seconds { get; set; }
        public string photo { get; set; }
    }

    public class Ingredient
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool frozen { get; set; }
        public string category { get; set; }
        public string baseType { get; set; }
        public string photo { get; set; }
    }

    public class MeasureIngredient
    {
        public int id { get; set; }
        public string measure { get; set; }
        public double quantity { get; set; }
        public Ingredient ingredient { get; set; }
    }

    public class Recipe
    {
        public int id { get; set; }
        public List<Task> tasks { get; set; }
        public List<MeasureIngredient> measureIngredients { get; set; }
        public string name { get; set; }
        public int score { get; set; }
        public string author { get; set; }
        public int difficulty { get; set; }
        public string photo { get; set; }
        public int portions { get; set; }
        public List<IngredientMeasure> ingredientList{ get;set;}
    }
    public class IngredientMeasure
    {
        public MeasureIngredient measure { get; set; }
        public Ingredient ingredient { get; set; }
    }
}
