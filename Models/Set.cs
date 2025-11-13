namespace FitnessTracker.Models
{
    /// <summary>
    /// Represents a single set of an exercise with specified weight and repetitions.
    /// </summary>
    public class Set
    {
        /// <summary>
        /// Gets or sets the unique identifier for the set.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key reference to the Exercise entity.
        /// </summary>
        public int ExerciseId { get; set; }

        /// <summary>
        /// Gets or sets the Exercise that this set belongs to.
        /// Navigation property for Entity Framework.
        /// </summary>
        public Exercise? Exercise { get; set; }

        /// <summary>
        /// Gets or sets the set number in the exercise sequence. Must be greater than 0.
        /// </summary>
        public int SetNumber { get; set; }

        /// <summary>
        /// Gets or sets the weight used for this set in pounds. Must be greater than 0.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the number of repetitions performed in this set. Must be greater than 0.
        /// </summary>
        public int Repetitions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Set"/> class.
        /// </summary>
        /// <param name="setNumber">The set number (must be greater than 0).</param>
        /// <param name="weight">The weight in pounds (must be greater than 0).</param>
        /// <param name="repetitions">The number of repetitions (must be greater than 0).</param>
        /// <exception cref="ArgumentException">
        /// Thrown when setNumber, weight, or repetitions are not greater than zero.
        /// </exception>
        public Set(int setNumber, double weight, int repetitions)
        {
            if (setNumber <= 0 || weight <= 0 || repetitions <= 0)
            {
                throw new ArgumentException("Set number must be positive, and weight and repetitions must be greater than zero.");
            }
            SetNumber = setNumber;
            Weight = weight;
            Repetitions = repetitions;
        }

        /// <summary>
        /// Parameterless constructor for Entity Framework Core.
        /// </summary>
        private Set() { }
    }
}
