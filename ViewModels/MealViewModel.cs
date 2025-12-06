using FitnessTracker.Commands;
using FitnessTracker.Data;
using FitnessTracker.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FitnessTracker.ViewModels
{
    /// <summary>
    /// View model for managing meal data entry, food item collection, and persistence.
    /// Provides commands and properties for creating meals with multiple food items for the current user.
    /// </summary>
    public class MealViewModel : BaseViewModel
    {
        private readonly FitnessTrackerContext _context;
        private readonly User _currentUser;
        private Food? _selectedFood;
        private double _servingsConsumed;
        private List<Food> _foodList = [];
        private string _mealName = string.Empty;
        private DateTime _mealDate;
        private ObservableCollection<FoodItem> _foodItems = new();

        /// <summary>
        /// Gets the command for saving the meal to the database.
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Gets the command for clearing all meal input fields and food items.
        /// </summary>
        public ICommand ClearCommand { get; }

        /// <summary>
        /// Gets the command for adding a food item to the meal's food item collection.
        /// </summary>
        public ICommand AddFoodItemCommand { get; }

        /// <summary>
        /// Gets the command for removing a food item from the meal's food item collection.
        /// </summary>
        public ICommand RemoveFoodItemCommand { get; }

        /// <summary>
        /// Gets or sets the list of available foods for selection.
        /// Populated from the database to provide options for the food selection dropdown.
        /// </summary>
        public List<Food> FoodList
        {
            get => _foodList;
            set => SetProperty(ref _foodList, value);
        }

        /// <summary>
        /// Gets or sets the currently selected food from the food list.
        /// Used in combination with <see cref="ServingsConsumed"/> to create a new food item.
        /// </summary>
        public Food? SelectedFood
        {
            get => _selectedFood;
            set => SetProperty(ref _selectedFood, value);
        }

        /// <summary>
        /// Gets or sets the number of servings consumed for the selected food.
        /// Must be greater than 0 to enable adding a food item.
        /// </summary>
        public double ServingsConsumed
        {
            get => _servingsConsumed;
            set => SetProperty(ref _servingsConsumed, value);
        }

        /// <summary>
        /// Gets or sets the name of the meal. Cannot be empty or whitespace to save.
        /// </summary>
        public string MealName
        {
            get => _mealName;
            set => SetProperty(ref _mealName, value);
        }

        /// <summary>
        /// Gets or sets the date when the meal was consumed.
        /// Cannot be in the future.
        /// </summary>
        public DateTime MealDate
        {
            get => _mealDate;
            set => SetProperty(ref _mealDate, value);
        }

        /// <summary>
        /// Gets or sets the collection of food items included in this meal.
        /// Uses <see cref="ObservableCollection{T}"/> to support UI data binding and automatic updates.
        /// </summary>
        public ObservableCollection<FoodItem> FoodItems
        {
            get => _foodItems;
            set => SetProperty(ref _foodItems, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MealViewModel"/> class.
        /// Sets up commands for meal operations, initializes the meal date to today,
        /// and loads available foods from the database.
        /// </summary>
        /// <param name="context">The database context for persisting meals.</param>
        /// <param name="currentUser">The current user for whom meals are being recorded.</param>
        public MealViewModel(FitnessTrackerContext context, User currentUser)
        {
            _context = context;
            _currentUser = currentUser;
            FoodList = [.. _context.Foods];
            MealDate = DateTime.Today;
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            ClearCommand = new RelayCommand(ExecuteClear);
            AddFoodItemCommand = new RelayCommand(ExecuteAddFoodItem, CanExecuteAddFoodItem);
            RemoveFoodItemCommand = new RelayCommand(ExecuteRemoveFoodItem, CanExecuteRemoveFoodItem);
        }

        /// <summary>
        /// Determines whether the remove food item command can execute.
        /// </summary>
        /// <param name="parameter">The food item to be removed.</param>
        /// <returns>True if the parameter is a valid <see cref="FoodItem"/>; otherwise, false.</returns>
        private bool CanExecuteRemoveFoodItem(object? parameter)
        {
            return parameter is FoodItem;
        }

        /// <summary>
        /// Executes the remove food item command to remove a food item from the meal.
        /// </summary>
        /// <param name="parameter">The <see cref="FoodItem"/> to remove from the collection.</param>
        private void ExecuteRemoveFoodItem(object? parameter)
        {
            if (parameter is FoodItem foodItem)
            {
                FoodItems.Remove(foodItem);
            }
        }

        /// <summary>
        /// Determines whether the add food item command can execute based on validation rules.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        /// <returns>True if a food is selected and servings consumed is greater than 0; otherwise, false.</returns>
        private bool CanExecuteAddFoodItem(object? parameter)
        {
            return SelectedFood != null && ServingsConsumed > 0;
        }

        /// <summary>
        /// Executes the add food item command to create a new food item and add it to the meal.
        /// Creates a <see cref="FoodItem"/> from the selected food and servings consumed,
        /// then resets the selection fields for the next entry.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        private void ExecuteAddFoodItem(object? parameter)
        {
            FoodItem foodItem = new FoodItem(SelectedFood!, ServingsConsumed);
            FoodItems.Add(foodItem);

            SelectedFood = null;
            ServingsConsumed = 0;
        }

        /// <summary>
        /// Executes the clear command to reset all meal input fields to their default values.
        /// Sets the meal name to empty, meal date to today, clears all food items,
        /// and resets food selection fields.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        private void ExecuteClear(object? parameter)
        {
            MealName = string.Empty;
            MealDate = DateTime.Today;
            FoodItems.Clear();
            ServingsConsumed = 0;
            SelectedFood = null;
        }

        /// <summary>
        /// Determines whether the save command can execute based on validation rules.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        /// <returns>
        /// True if the meal name is not empty, the meal date is not in the future,
        /// and at least one food item has been added; otherwise, false.
        /// </returns>
        private bool CanExecuteSave(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(MealName) &&
                   MealDate <= DateTime.Today &&
                   FoodItems.Count > 0;
        }

        /// <summary>
        /// Executes the save command to persist the meal to the database.
        /// Creates a new <see cref="Meal"/> with the current input values and food items,
        /// adds it to the current user's meal collection, saves to the database,
        /// and clears all input fields after successful save.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        private void ExecuteSave(object? parameter)
        {
            var meal = new Meal(MealName, MealDate, FoodItems.ToList());
            _currentUser.Meals.Add(meal);
            _context.SaveChanges();
            ExecuteClear(null);
        }
    }
}