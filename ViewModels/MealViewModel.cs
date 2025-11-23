using FitnessTracker.Commands;
using FitnessTracker.Data;
using FitnessTracker.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FitnessTracker.ViewModels
{
    public class MealViewModel : BaseViewModel
    {
        private readonly FitnessTrackerContext _context;
        private readonly User _currentUser;
        private string _mealName = string.Empty;
        private DateTime _mealDate;
        private ObservableCollection<FoodItem> _foodItems = [];
        public ICommand SaveCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand AddFoodItemCommand { get; }
        public ICommand RemoveFoodItemCommand { get; }

        public string MealName
        {
            get => _mealName;
            set => SetProperty(ref _mealName, value);
        }

        public DateTime MealDate
        {
            get => _mealDate;
            set => SetProperty(ref _mealDate, value);
        }   

        public ObservableCollection<FoodItem> FoodItems
        {
            get => _foodItems;
            set => SetProperty(ref _foodItems, value);
        }

        public MealViewModel(FitnessTrackerContext context, User currentUser)
        {
            _context = context;
            _currentUser = currentUser;
            MealDate = DateTime.Today;
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            ClearCommand = new RelayCommand(ExecuteClear);
            AddFoodItemCommand = new RelayCommand(ExecuteAddFoodItem);
            RemoveFoodItemCommand = new RelayCommand(ExecuteRemoveFoodItem, CanExecuteRemoveFoodItem);

        }

        private bool CanExecuteRemoveFoodItem(object? obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteRemoveFoodItem(object? obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteAddFoodItem(object? obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteClear(object? parameter)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteSave(object? parameter)
        {
            throw new NotImplementedException();
        }

        private void ExecuteSave(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
