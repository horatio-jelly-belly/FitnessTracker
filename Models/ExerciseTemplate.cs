namespace FitnessTracker.Models
{
    /// <summary>
    /// Represents a predefined template for an exercise that can be used to create workout exercises.
    /// </summary>
    public class ExerciseTemplate
    {
        /// <summary>
        /// Gets or sets the unique identifier for the exercise template.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the exercise template.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the foreign key reference to the ExerciseCategory entity.
        /// </summary>
        public int ExerciseCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category of this exercise template (e.g., chest, legs, arms).
        /// </summary>
        public ExerciseCategory ExerciseCategory { get; set; } = null!;

        /// <summary>
        /// Parameterless constructor for Entity Framework Core.
        /// </summary>
        private ExerciseTemplate() { }
    }
}
