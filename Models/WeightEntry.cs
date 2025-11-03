namespace FitnessTracker.Models
{
    /// <summary>
    /// Represents a single weight entry with date, weight, and optional body fat percentage.
    /// </summary>
    public class WeightEntry
    {
        private double _weight;
        private double _bodyFatPercentage;

        /// <summary>
        /// Gets or sets the unique identifier for the weight entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the date when this weight measurement was recorded.
        /// Cannot be in the future.
        /// </summary>
        public DateTime EntryDate { get; init; }

        /// <summary>
        /// Gets or sets the weight in pounds. Must be greater than 0.
        /// </summary>
        public double Weight 
        { 
            get => _weight; 
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Weight), "Weight must be greater than 0.");
                _weight = value;
            }
        }

        /// <summary>
        /// Gets or sets the body fat percentage. Must be between 0 and 100.
        /// </summary>
        public double BodyFatPercentage 
        { 
            get => _bodyFatPercentage; 
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(BodyFatPercentage), "Body fat percentage must be between 0 and 100.");
                _bodyFatPercentage = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightEntry"/> class.
        /// </summary>
        /// <param name="entryDate">The date of the weight entry (cannot be in the future).</param>
        /// <param name="weight">The weight in pounds (must be greater than 0).</param>
        /// <param name="bodyFatPercentage">Optional body fat percentage (0-100).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when parameters are outside valid ranges.
        /// </exception>
        public WeightEntry(DateTime entryDate, double weight, double bodyFatPercentage = 0)
        {
            // Validate date here because EntryDate is init-only
            if (entryDate > DateTime.Today)
                throw new ArgumentOutOfRangeException(nameof(entryDate), "Entry date cannot be in the future.");

            EntryDate = entryDate;
            Weight = weight;
            BodyFatPercentage = bodyFatPercentage;
        }

        /// <summary>
        /// Parameterless constructor for Entity Framework Core.
        /// </summary>
        private WeightEntry() { }
    }
}