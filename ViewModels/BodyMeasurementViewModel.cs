using FitnessTracker.Data;
using FitnessTracker.Models;
using System.Windows.Input;
using FitnessTracker.Commands;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.ViewModels
{
    /// <summary>
    /// View model for managing body measurement data entry and validation.
    /// Provides commands and properties for recording body measurements for the current user.
    /// </summary>
    public class BodyMeasurementViewModel : BaseViewModel
    {
        private DateTime _measurementDate;
        private double _waistSize;
        private double _chestSize;
        private double _armSize;
        private double _thighSize;
        private double _hipSize;
        private readonly FitnessTrackerContext _context;
        private readonly User _currentUser;

        /// <summary>
        /// Gets the command for saving body measurements to the database.
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Gets the command for clearing all measurement input fields.
        /// </summary>
        public ICommand ClearCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyMeasurementViewModel"/> class.
        /// </summary>
        /// <param name="context">The database context for saving measurements.</param>
        /// <param name="currentUser">The user for whom measurements are being recorded.</param>
        public BodyMeasurementViewModel(FitnessTrackerContext context, User currentUser)
        {
            _context = context;
            _currentUser = currentUser;
            MeasurementDate = DateTime.Today;
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            ClearCommand = new RelayCommand(ExecuteClear);
        }

        /// <summary>
        /// Determines whether the save command can execute based on validation rules.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        /// <returns>
        /// True if the measurement date is not in the future and all measurement values are greater than 0; otherwise, false.
        /// </returns>
        private bool CanExecuteSave(object? parameter)
        {
            return MeasurementDate <= DateTime.Today &&
                   WaistSize > 0 &&
                   ChestSize > 0 &&
                   ArmSize > 0 &&
                   ThighSize > 0 &&
                   HipSize > 0;
        }

        /// <summary>
        /// Executes the save command to persist body measurements to the database.
        /// Creates a new <see cref="BodyMeasurement"/> and adds it to the current user's collection.
        /// Clears all input fields after successful save.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        private void ExecuteSave(object? parameter)
        {
            var measurement = new BodyMeasurement(MeasurementDate, WaistSize, HipSize, ChestSize, ArmSize, ThighSize);
            _currentUser.BodyMeasurements.Add(measurement);
            _context.SaveChanges();
            ExecuteClear(null);
        }

        /// <summary>
        /// Executes the clear command to reset all measurement input fields to their default values.
        /// Sets the measurement date to the current date and all size values to 0.
        /// </summary>
        /// <param name="parameter">Command parameter (not used).</param>
        private void ExecuteClear(object? parameter)
        {
            MeasurementDate = DateTime.Today;
            WaistSize = 0;
            ChestSize = 0;
            ArmSize = 0;
            ThighSize = 0;
            HipSize = 0;
        }

        /// <summary>
        /// Gets or sets the date when the measurements are being taken.
        /// Cannot be in the future.
        /// </summary>
        public DateTime MeasurementDate
        {
            get => _measurementDate;
            set => SetProperty(ref _measurementDate, value);
        }

        /// <summary>
        /// Gets or sets the waist size in inches. Must be greater than 0 to save.
        /// </summary>
        public double WaistSize
        {
            get => _waistSize;
            set => SetProperty(ref _waistSize, value);
        }

        /// <summary>
        /// Gets or sets the chest size in inches. Must be greater than 0 to save.
        /// </summary>
        public double ChestSize
        {
            get => _chestSize;
            set => SetProperty(ref _chestSize, value);
        }

        /// <summary>
        /// Gets or sets the arm size in inches. Must be greater than 0 to save.
        /// </summary>
        public double ArmSize
        {
            get => _armSize;
            set => SetProperty(ref _armSize, value);
        }

        /// <summary>
        /// Gets or sets the thigh size in inches. Must be greater than 0 to save.
        /// </summary>
        public double ThighSize
        {
            get => _thighSize;
            set => SetProperty(ref _thighSize, value);
        }

        /// <summary>
        /// Gets or sets the hip size in inches. Must be greater than 0 to save.
        /// </summary>
        public double HipSize
        {
            get => _hipSize;
            set => SetProperty(ref _hipSize, value);
        }
    }
}
