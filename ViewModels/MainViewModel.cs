using FitnessTracker.Data;
using FitnessTracker.Models;

namespace FitnessTracker.ViewModels
{
    /// <summary>
    /// Represents the main view model for the application, coordinating child view models
    /// and managing the database context and current user.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private readonly FitnessTrackerContext _context;
        private readonly User _currentUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// Creates the database context, loads or creates the current user, and initializes child view models.
        /// </summary>
        public MainViewModel()
        {
            _context = new FitnessTrackerContext();
            _currentUser = _context.Users.FirstOrDefault() ?? CreateDefaultUser();

            // Child ViewModels
            WeightEntryVM = new WeightEntryViewModel(_context, _currentUser);
            BodyMeasurementVM = new BodyMeasurementViewModel(_context, _currentUser);
        }

        /// <summary>
        /// Gets the view model for weight entry functionality.
        /// </summary>
        public WeightEntryViewModel WeightEntryVM { get; }

        /// <summary>
        /// Gets the view model for body measurement functionality.
        /// </summary>
        public BodyMeasurementViewModel BodyMeasurementVM { get; }

        /// <summary>
        /// Creates a default user for testing purposes when no users exist in the database.
        /// The default user is configured with predefined values and saved to the database.
        /// </summary>
        /// <returns>The newly created default user.</returns>
        private User CreateDefaultUser()
        {
            // default user for testing
            var user = new User(
                heightFeet: 6,
                heightInches: 0,
                dateofBirth: new DateTime(1981, 3, 14),
                goal: FitnessGoal.WeightLoss,
                gender: "Male"
            );

            // Add to context and save
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
    }
}