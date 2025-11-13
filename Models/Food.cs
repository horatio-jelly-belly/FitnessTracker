using System;

namespace FitnessTracker.Models
{
    /// <summary>
    /// Represents a food with nutritional information per serving.
    /// </summary>
    public class Food
    {
        /// <summary>
        /// Gets or sets the unique identifier for the food.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the food.
        /// </summary>
        public string FoodName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the serving size in grams. Must be greater than 0.
        /// </summary>
        public double ServingSize { get; set; }

        /// <summary>
        /// Gets or sets the calories per serving.
        /// </summary>
        public double CaloriesPerServing { get; set; }

        /// <summary>
        /// Gets or sets the protein per serving in grams.
        /// </summary>
        public double ProteinPerServing { get; set; }

        /// <summary>
        /// Gets or sets the carbohydrates per serving in grams.
        /// </summary>
        public double CarbohydratesPerServing { get; set; }

        /// <summary>
        /// Gets or sets the fats per serving in grams.
        /// </summary>
        public double FatsPerServing { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Food"/> class.
        /// </summary>
        /// <param name="foodName">Name of the food.</param>
        /// <param name="servingSize">Serving size in grams (must be greater than 0).</param>
        /// <param name="caloriesPerServing">Calories per serving.</param>
        /// <param name="proteinPerServing">Protein per serving in grams.</param>
        /// <param name="carbohydratesPerServing">Carbohydrates per serving in grams.</param>
        /// <param name="fatsPerServing">Fats per serving in grams.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when serving size is less than or equal to zero.
        /// </exception>
        public Food(string foodName, double servingSize, double caloriesPerServing, double proteinPerServing, double carbohydratesPerServing, double fatsPerServing)
        {
            if (!IsValidServingSize(servingSize))
            {
                throw new ArgumentException("Serving size must be greater than zero.", nameof(servingSize));
            }

            FoodName = foodName;
            ServingSize = servingSize;
            CaloriesPerServing = caloriesPerServing;
            ProteinPerServing = proteinPerServing;
            CarbohydratesPerServing = carbohydratesPerServing;
            FatsPerServing = fatsPerServing;
        }

        /// <summary>
        /// Validates that the serving size is greater than zero.
        /// </summary>
        /// <param name="size">The serving size to validate.</param>
        /// <returns>True if the serving size is valid (greater than 0); otherwise, false.</returns>
        public bool IsValidServingSize(double size)
        {
            return size > 0;
        }

        /// <summary>
        /// Returns a string representation of the food with its nutritional information.
        /// </summary>
        /// <returns>A formatted string containing the food name, serving size, and nutritional values.</returns>
        public override string ToString()
        {
            return $"{FoodName}: {ServingSize}g, {CaloriesPerServing} kcal, {ProteinPerServing}g protein, {CarbohydratesPerServing}g carbs, {FatsPerServing}g fats";
        }

        /// <summary>
        /// Parameterless constructor for Entity Framework Core.
        /// </summary>
        private Food() { }
    }
}
