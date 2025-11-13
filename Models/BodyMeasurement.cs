namespace FitnessTracker.Models
{
    /// <summary>
    /// Represents body measurements taken at a specific date for tracking physical progress.
    /// </summary>
    public class BodyMeasurement
    {
        /// <summary>
        /// Gets or sets the unique identifier for the body measurement.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key reference to the User entity.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the User that owns this body measurement.
        /// Navigation property for Entity Framework.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Gets or sets the date when the measurements were taken.
        /// Cannot be in the future.
        /// </summary>
        public DateTime MeasurementDate { get; set; }

        /// <summary>
        /// Gets or sets the waist size in inches. Must be greater than 0.
        /// </summary>
        public double WaistSize { get; set; }

        /// <summary>
        /// Gets or sets the chest size in inches. Must be greater than 0.
        /// </summary>
        public double ChestSize { get; set; }

        /// <summary>
        /// Gets or sets the arm size in inches. Must be greater than 0.
        /// </summary>
        public double ArmSize { get; set; }

        /// <summary>
        /// Gets or sets the thigh size in inches. Must be greater than 0.
        /// </summary>
        public double ThighSize { get; set; }

        /// <summary>
        /// Gets or sets the hip size in inches. Must be greater than 0.
        /// </summary>
        public double HipSize { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyMeasurement"/> class.
        /// </summary>
        /// <param name="measurementDate">The date of the measurement (cannot be in the future).</param>
        /// <param name="waistSize">Waist size in inches (must be greater than 0).</param>
        /// <param name="hipSize">Hip size in inches (must be greater than 0).</param>
        /// <param name="chestSize">Chest size in inches (must be greater than 0).</param>
        /// <param name="armSize">Arm size in inches (must be greater than 0).</param>
        /// <param name="thighSize">Thigh size in inches (must be greater than 0).</param>
        /// <exception cref="ArgumentException">
        /// Thrown when measurementDate is in the future or any measurement size is not greater than zero.
        /// </exception>
        public BodyMeasurement(DateTime measurementDate, double waistSize, double hipSize,
                       double chestSize, double armSize, double thighSize)
        {
            if (measurementDate > DateTime.Today)
                throw new ArgumentException("Measurement date cannot be in the future.", nameof(measurementDate));

            if (waistSize <= 0 || hipSize <= 0 || chestSize <= 0 || armSize <= 0 || thighSize <= 0)
                throw new ArgumentException("All measurement sizes must be greater than zero.");

            MeasurementDate = measurementDate;
            WaistSize = waistSize;
            HipSize = hipSize;
            ChestSize = chestSize;
            ArmSize = armSize;
            ThighSize = thighSize;
        }

        /// <summary>
        /// Parameterless constructor for Entity Framework Core.
        /// </summary>
        private BodyMeasurement() { }
    }
}