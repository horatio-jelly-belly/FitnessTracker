using FitnessTracker.Commands;
using FitnessTracker.Data;
using FitnessTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;

namespace FitnessTracker.ViewModels
{
    /// <summary>
    /// View model for managing weight entry data input and persistence.
    /// Handles user input for weight tracking including date, weight, and body fat percentage.
    /// </summary>
    public class WeightEntryViewModel : BaseViewModel
    {
        private DateTime _entryDate;
        private double _weight;
        private double _bodyFatPercentage;
        private readonly FitnessTrackerContext _context;
        private readonly User _currentUser;

        /// <summary>
        /// Gets the command to save a new weight entry to the database.
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Gets the command to clear all input fields.
        /// </summary>
        public ICommand ClearCommand { get; }

        /// <summary>
        /// Gets or sets the date of the weight entry.
        /// </summary>
        public DateTime EntryDate
        {
            get => _entryDate;
            set => SetProperty(ref _entryDate, value);
        }

        /// <summary>
        /// Gets or sets the weight in pounds. Must be greater than 0 to enable save.
        /// </summary>
        public double Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        /// <summary>
        /// Gets or sets the body fat percentage. Must be greater than or equal to 0 to enable save.
        /// </summary>
        public double BodyFatPercentage
        {
            get => _bodyFatPercentage;
            set => SetProperty(ref _bodyFatPercentage, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightEntryViewModel"/> class.
        /// Sets up commands for saving and clearing weight entries, initializes the entry date to today,
        /// and stores references to the database context and current user.
        /// </summary>
        /// <param name="context">The database context for persisting weight entries.</param>
        /// <param name="currentUser">The current user whose weight is being tracked.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when context or currentUser is null.
        /// </exception>
        public WeightEntryViewModel(FitnessTrackerContext context, User currentUser)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

            EntryDate = DateTime.Today;
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            ClearCommand = new RelayCommand(ExecuteClear);
        }

        /// <summary>
        /// Determines whether the save command can execute.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        /// <returns>True if weight is greater than 0 and body fat percentage is non-negative; otherwise, false.</returns>
        private bool CanExecuteSave(object? parameter)
        {
            return Weight > 0 && BodyFatPercentage >= 0;
        }

        /// <summary>
        /// Executes the save command to persist a new weight entry to the database.
        /// Resets the form fields after a successful save.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        private void ExecuteSave(object? parameter)
        {
            WeightEntry newEntry = new WeightEntry(EntryDate, Weight, BodyFatPercentage);
            _currentUser.WeightEntries.Add(newEntry);
            _context.SaveChanges();

            // Reset form after successful save
            Weight = 0;
            BodyFatPercentage = 0;
        }

        /// <summary>
        /// Executes the clear command to reset all input fields to their default values.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        private void ExecuteClear(object? parameter)
        {
            EntryDate = DateTime.Today;
            Weight = 0;
            BodyFatPercentage = 0;
        }
    }
}
