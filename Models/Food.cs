namespace FitnessTracker.Models
{
    public class Food
    {
        /// <summary>
        /// Gets or sets the unique identifier for the food.
        /// </summary>
        public int Id { get; set; }
        public string FoodName { get; set; } = string.Empty;
        public double ServingSize { get; set; } // in grams
        public double CaloriesPerServing { get; set; }
        public double ProteinPerServing { get; set; } // in grams
        public double CarbohydratesPerServing { get; set; } // in grams
        public double FatsPerServing { get; set; } // in grams
    }
}
