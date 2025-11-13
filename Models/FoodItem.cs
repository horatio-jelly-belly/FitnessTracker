using System;

namespace FitnessTracker.Models
{
    /// <summary>
    /// Represents a food item consumed in a meal with calculated nutritional values based on servings.
    /// </summary>
    public class FoodItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the food item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key reference to the Food entity.
        /// </summary>
        public int FoodId { get; set; }

        /// <summary>
        /// Gets or sets the foreign key reference to the Meal entity.
        /// </summary>
        public int MealId { get; set; }

        /// <summary>
        /// Gets or sets the Meal entity that this food item is associated with.
        /// Navigation property for Entity Framework.
        /// </summary>
        public Meal? Meal { get; set; }

        /// <summary>
        /// Gets or sets the Food entity that this food item is based on.
        /// Navigation property for Entity Framework.
        /// </summary>
        public Food Food { get; set; } = null!;

        /// <summary>
        /// Gets or sets the number of servings consumed.
        /// </summary>
        public double ServingsConsumed { get; set; }

        /// <summary>
        /// Gets or sets the total calories calculated from servings consumed.
        /// </summary>
        public double Calories { get; set; }

        /// <summary>
        /// Gets or sets the total protein in grams calculated from servings consumed.
        /// </summary>
        public double Protein { get; set; }

        /// <summary>
        /// Gets or sets the total carbohydrates in grams calculated from servings consumed.
        /// </summary>
        public double Carbohydrates { get; set; }

        /// <summary>
        /// Gets or sets the total fats in grams calculated from servings consumed.
        /// </summary>
        public double Fats { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodItem"/> class.
        /// </summary>
        /// <param name="food">The food entity (cannot be null).</param>
        /// <param name="servingConsumed">The number of servings consumed.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when food is null.
        /// </exception>
        public FoodItem(Food food, double servingsConsumed)
        {
            Food = food ?? throw new ArgumentNullException(nameof(food));
            FoodId = food.Id;
            ServingsConsumed = servingsConsumed;
            CalculateNutrition();
        }

        /// <summary>
        /// Calculates the nutritional values (calories, protein, carbs, fats) based on 
        /// the food's per-serving values and the number of servings consumed.
        /// </summary>
        public void CalculateNutrition()
        {
            if (Food == null) return;

            Calories = Food.CaloriesPerServing * ServingsConsumed;
            Protein = Food.ProteinPerServing * ServingsConsumed;
            Carbohydrates = Food.CarbohydratesPerServing * ServingsConsumed;
            Fats = Food.FatsPerServing * ServingsConsumed;
        }

        /// <summary>
        /// Parameterless constructor for Entity Framework Core.
        /// </summary>
        private FoodItem() { }
    }
}
