using System;

namespace FitnessTracker.Models
{
    /// <summary>
    /// Represents a category for classifying exercises (e.g., Strength, Cardio, Flexibility).
    /// </summary>
    public class ExerciseCategory
    {
        /// <summary>
        /// Gets or sets the unique identifier for the exercise category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the exercise category.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a description of the exercise category.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of exercises belonging to this category.
        /// </summary>
        public List<Exercise> Exercises { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="ExerciseCategory"/> class.
        /// </summary>
        /// <param name="name">The name of the exercise category.</param>
        /// <param name="description">A description of the exercise category.</param>
        public ExerciseCategory(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Parameterless constructor for Entity Framework Core.
        /// </summary>
        private ExerciseCategory() { }
    }
}
