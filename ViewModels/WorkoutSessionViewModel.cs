using FitnessTracker.Commands;
using FitnessTracker.Data;
using FitnessTracker.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FitnessTracker.ViewModels
{
    /// <summary>
    /// View model for managing workout session data entry, exercise collection, and persistence.
    /// Provides commands and properties for creating workout sessions with multiple exercises and sets for the current user.
    /// </summary>
    public class WorkoutSessionViewModel : BaseViewModel
    {
        private readonly FitnessTrackerContext _context;
        private readonly User _currentUser;
        private DateTime _sessionDate;
        private int _setNumber;
        private int _repetitions;
        private double _weight;
        private ExerciseTemplate? _selectedTemplate;        
        private ExerciseCategory? _selectedCategory;
        private ObservableCollection<ExerciseTemplate> _exerciseTemplateList = [];
        private ObservableCollection<ExerciseCategory> _exerciseCategoriesList = [];
        private ObservableCollection<Set> _sets = new();
        private ObservableCollection<Exercise> _completedExercises = [];
        private readonly List<ExerciseTemplate> _allTemplates;

        /// <summary>
        /// Gets the command for adding a new set to the current exercise.
        /// </summary>
        public ICommand AddSetCommand { get; }

        /// <summary>
        /// Gets the command for completing the current exercise and adding it to the completed exercises collection.
        /// </summary>
        public ICommand CompleteExerciseCommand { get; }

        /// <summary>
        /// Gets the command for saving the workout session to the database.
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Gets the command for clearing all workout session input fields and completed exercises.
        /// </summary>
        public ICommand ClearCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkoutSessionViewModel"/> class.
        /// Sets up commands for workout session operations, initializes the session date to today,
        /// and loads available exercise categories and templates from the database.
        /// </summary>
        /// <param name="context">The database context for persisting workout sessions.</param>
        /// <param name="currentUser">The current user for whom workout sessions are being recorded.</param>
        public WorkoutSessionViewModel(FitnessTrackerContext context, User currentUser)
        {
            _sessionDate = DateTime.Today;
            _context = context;
            _currentUser = currentUser;
            ExerciseCategoriesList = [.. _context.ExerciseCategories];
            _allTemplates = [.. _context.ExerciseTemplates];
            AddSetCommand = new RelayCommand(AddSet, CanAddSet);
            CompleteExerciseCommand = new RelayCommand(CompleteExercise, CanCompleteExercise);
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            ClearCommand = new RelayCommand(ExecuteClear);
        }

        /// <summary>
        /// Executes the clear command to reset all workout session input fields to their default values.
        /// Clears the form fields and removes all completed exercises.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        private void ExecuteClear(object? parameter)
        {
            ClearForm();
            CompletedExercises.Clear();
            
        }

        /// <summary>
        /// Determines whether the save command can execute based on validation rules.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        /// <returns>
        /// True if at least one exercise has been completed and the session date is not in the future;
        /// otherwise, false.
        /// </returns>
        private bool CanExecuteSave(object? parameter)
        {
            return CompletedExercises.Count > 0 && SessionDate <= DateTime.Today;
        }

        /// <summary>
        /// Executes the save command to persist the workout session to the database.
        /// Creates a new <see cref="WorkoutSession"/> with the current session date and completed exercises,
        /// adds it to the current user's workout session collection, saves to the database,
        /// and clears all input fields after successful save.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        private void ExecuteSave(object? parameter)
        {
            var workoutSession = new WorkoutSession(SessionDate, CompletedExercises.ToList());
            _currentUser.WorkoutSessions.Add(workoutSession);
            _context.SaveChanges();
            ExecuteClear(null);

        }

        /// <summary>
        /// Determines whether the complete exercise command can execute based on validation rules.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        /// <returns>
        /// True if an exercise template and category are selected and at least one set has been added;
        /// otherwise, false.
        /// </returns>
        private bool CanCompleteExercise(object? parameter)
        {
            return SelectedTemplate != null &&
                   SelectedCategory != null &&
                   Sets.Count > 0;
        }

        /// <summary>
        /// Executes the complete exercise command to create a new exercise and add it to the completed exercises collection.
        /// Creates an <see cref="Exercise"/> from the selected template and category with all current sets,
        /// then clears the form for the next exercise entry.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        private void CompleteExercise(object? parameter)
        {
            var exercise = new Exercise(SelectedTemplate!.Name, SelectedCategory!)
            {
                Sets = [.. Sets]
            };
            CompletedExercises.Add(exercise);
            ClearForm();

        }

        /// <summary>
        /// Determines whether the add set command can execute based on validation rules.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        /// <returns>
        /// True if an exercise template is selected and set number, weight, and repetitions are all greater than 0;
        /// otherwise, false.
        /// </returns>
        private bool CanAddSet(object? parameter)
        {
            return SelectedTemplate != null &&
                   SetNumber > 0 &&
                   Weight > 0 &&
                   Repetitions > 0;
        }

        /// <summary>
        /// Executes the add set command to create a new set and add it to the current exercise's set collection.
        /// Creates a <see cref="Set"/> from the current set number, weight, and repetitions,
        /// then increments the set number for the next entry.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        private void AddSet(object? parameter)
        {
            var newSet = new Set(SetNumber, Weight, Repetitions);
            Sets.Add(newSet);
            SetNumber++;
        }

        /// <summary>
        /// Gets or sets the current set number for the exercise being created.
        /// Must be greater than 0 to enable adding a set.
        /// </summary>
        public int SetNumber
        {
            get => _setNumber;
            set => SetProperty(ref _setNumber, value);
        }

        /// <summary>
        /// Gets or sets the number of repetitions for the current set.
        /// Must be greater than 0 to enable adding a set.
        /// </summary>
        public int Repetitions
        {
            get => _repetitions;
            set => SetProperty(ref _repetitions, value);
        }

        /// <summary>
        /// Gets or sets the weight used for the current set in pounds.
        /// Must be greater than 0 to enable adding a set.
        /// </summary>
        public double Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        /// <summary>
        /// Gets or sets the collection of exercise templates available for selection.
        /// Filtered based on the selected exercise category.
        /// Uses <see cref="ObservableCollection{T}"/> to support UI data binding and automatic updates.
        /// </summary>
        public ObservableCollection<ExerciseTemplate> ExerciseTemplateList
        {
            get => _exerciseTemplateList;
            set => SetProperty(ref _exerciseTemplateList, value);
        }

        /// <summary>
        /// Gets or sets the collection of exercise categories available for selection.
        /// Populated from the database to provide options for the category selection dropdown.
        /// Uses <see cref="ObservableCollection{T}"/> to support UI data binding and automatic updates.
        /// </summary>
        public ObservableCollection<ExerciseCategory> ExerciseCategoriesList
        {
            get => _exerciseCategoriesList;
            set => SetProperty(ref _exerciseCategoriesList, value);
        }

        /// <summary>
        /// Gets or sets the currently selected exercise template.
        /// Used in combination with <see cref="SelectedCategory"/> to create a new exercise.
        /// </summary>
        public ExerciseTemplate? SelectedTemplate
        {
            get => _selectedTemplate;
            set => SetProperty(ref _selectedTemplate, value);
        }

        /// <summary>
        /// Gets or sets the currently selected exercise category.
        /// When changed, clears the selected template and filters the exercise template list
        /// to show only templates belonging to this category.
        /// </summary>
        public ExerciseCategory? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                SetProperty(ref _selectedCategory, value);
                SelectedTemplate = null;
                ExerciseTemplateList = new ObservableCollection<ExerciseTemplate>(_allTemplates.Where(t => t.ExerciseCategoryId == _selectedCategory?.Id));
            }
        }

        /// <summary>
        /// Gets or sets the date when the workout session was performed.
        /// Cannot be in the future.
        /// </summary>
        public DateTime SessionDate
        {
            get => _sessionDate;
            set => SetProperty(ref _sessionDate, value);
        }

        /// <summary>
        /// Gets or sets the collection of sets for the current exercise being created.
        /// Uses <see cref="ObservableCollection{T}"/> to support UI data binding and automatic updates.
        /// </summary>
        public ObservableCollection<Set> Sets
        {
            get => _sets;
            set => SetProperty(ref _sets, value);
        }

        /// <summary>
        /// Gets or sets the collection of completed exercises for this workout session.
        /// Uses <see cref="ObservableCollection{T}"/> to support UI data binding and automatic updates.
        /// </summary>
        public ObservableCollection<Exercise> CompletedExercises
        {
            get => _completedExercises;
            set => SetProperty(ref _completedExercises, value);
        }

        /// <summary>
        /// Resets all form input fields to their default values for entering a new exercise.
        /// Sets the session date to today, clears category and template selections,
        /// and resets repetitions, weight, set number, and the sets collection.
        /// </summary>
        private void ClearForm()
        {
            SessionDate = DateTime.Today;
            SelectedCategory = null;
            SelectedTemplate = null;
            Repetitions = 0;
            Weight = 0;
            SetNumber = 0;
            Sets.Clear();
        }
    }
}
