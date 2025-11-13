using System.Text;

namespace FitnessTracker.Models
{
    /// <summary>
    /// Represents a meal containing one or more food items with nutritional information.
    /// </summary>
    public class Meal
    {
        private string _mealName = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier for the meal.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key reference to the User entity.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the User that owns this meal.
        /// Navigation property for Entity Framework.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Gets or sets the name of the meal. Cannot be null or empty.
        /// </summary>
        public string MealName 
        { 
            get => _mealName; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Meal name cannot be empty.", nameof(MealName));
                _mealName = value;
            }
        }

        /// <summary>
        /// Gets the date and time when the meal was consumed.
        /// Cannot be in the future.
        /// </summary>
        public DateTime MealDate { get; init; }

        /// <summary>
        /// Gets or sets the collection of food items in this meal.
        /// </summary>
        public List<FoodItem> FoodItems { get; set; } = []; 

        /// <summary>
        /// Initializes a new instance of the <see cref="Meal"/> class.
        /// </summary>
        /// <param name="name">The name of the meal (cannot be null or empty).</param>
        /// <param name="date">The date and time of the meal (cannot be in the future).</param>
        /// <param name="items">The list of food items (must contain at least one item).</param>
        /// <exception cref="ArgumentException">
        /// Thrown when name is null/empty or items list is null/empty.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when date is in the future.
        /// </exception>
        public Meal(string name, DateTime date, List<FoodItem> items = null) 
        { 
            if (date > DateTime.Today)
                throw new ArgumentOutOfRangeException(nameof(date), "Meal date cannot be in the future.");

            MealName = name;
            MealDate = date;
            FoodItems = items ?? [];
        }

        /// <summary>
        /// Adds a food item to the meal.
        /// </summary>
        /// <param name="item">The food item to add.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when item is null.
        /// </exception>
        public void AddFoodItem(FoodItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Food item cannot be null.");
            FoodItems.Add(item);
        }

        /// <summary>
        /// Removes a food item from the meal.
        /// </summary>
        /// <param name="item">The food item to remove.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when item is null.
        /// </exception>
        public void RemoveFoodItem(FoodItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Food item cannot be null.");
            FoodItems.Remove(item);
        }

        /// <summary>
        /// Calculates the total calories from all food items in the meal. If a food item is null, it contributes 0 calories.
        /// </summary>
        /// <returns>The sum of calories from all food items.</returns>
        public double CalculateTotalCalories()
        {
            return FoodItems?.Sum(fi => fi?.Calories ?? 0) ?? 0;
        }

        /// <summary>
        /// Calculates the total protein from all food items in the meal.
        /// </summary>
        /// <returns>The sum of protein in grams from all food items.</returns>
        public double CalculateTotalProtein()
        {
            return FoodItems?.Sum(fi => fi?.Protein ?? 0) ?? 0;
        }

        /// <summary>
        /// Calculates the total carbohydrates from all food items in the meal.
        /// </summary>
        /// <returns>The sum of carbohydrates in grams from all food items.</returns>
        public double CalculateTotalCarbohydrates()
        {
            return FoodItems?.Sum(fi => fi?.Carbohydrates ?? 0) ?? 0;
        }

        /// <summary>
        /// Calculates the total fats from all food items in the meal.
        /// </summary>
        /// <returns>The sum of fats in grams from all food items.</returns>
        public double CalculateTotalFats()
        {
            return FoodItems?.Sum(fi => fi?.Fats ?? 0) ?? 0;
        }

        /// <summary>
        /// Generates a detailed summary of the meal including all food items and nutritional totals.
        /// </summary>
        /// <returns>A formatted string containing meal details and nutritional information.</returns>
        public string GetMealSummary()
        {
            StringBuilder summary = new StringBuilder();
            summary.AppendLine($"Meal: {MealName} on {MealDate.ToShortDateString()}");
            summary.AppendLine("Food Items:");
            foreach (var item in FoodItems.Where(fi => fi != null))
            {
                    summary.AppendLine($"- {item.Food?.FoodName ?? "Unknown"}: {item.Calories} kcal, {item.Protein}g protein, {item.Carbohydrates}g carbs, {item.Fats}g fats");
            }
            summary.AppendLine($"Total Calories: {CalculateTotalCalories()} kcal");
            summary.AppendLine($"Total Protein: {CalculateTotalProtein()} g");
            summary.AppendLine($"Total Carbohydrates: {CalculateTotalCarbohydrates()} g");
            summary.AppendLine($"Total Fats: {CalculateTotalFats()} g");
            return summary.ToString();
        }

        /// <summary>
        /// Parameterless constructor for Entity Framework Core.
        /// </summary>
        private Meal() { }
    }
}
