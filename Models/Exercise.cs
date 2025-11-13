using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessTracker.Models
{
    /// <summary>
    /// Represents an exercise performed in a workout session with multiple sets.
    /// </summary>
    public class Exercise
    {
        /// <summary>
        /// Gets or sets the unique identifier for the exercise.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the exercise.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the foreign key reference to the WorkoutSession entity.
        /// </summary>
        public int WorkoutSessionId { get; set; }

        /// <summary>
        /// Gets or sets the WorkoutSession that this exercise belongs to.
        /// Navigation property for Entity Framework.
        /// </summary>
        public WorkoutSession? WorkoutSession { get; set; }

        /// <summary>
        /// Gets or sets the foreign key reference to the ExerciseCategory entity.
        /// </summary>
        public int ExerciseCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category of this exercise (e.g., strength, cardio).
        /// </summary>
        public ExerciseCategory ExerciseCategory { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of sets performed for this exercise.
        /// </summary>
        public List<Set> Sets { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="Exercise"/> class.
        /// </summary>
        /// <param name="name">The name of the exercise.</param>
        /// <param name="exerciseCategory">The category of the exercise.</param>
        public Exercise(string name, ExerciseCategory exerciseCategory)
        {
            Name = name;
            ExerciseCategory = exerciseCategory;
            ExerciseCategoryId = exerciseCategory.Id;
        }

        /// <summary>
        /// Adds a new set to the exercise.
        /// </summary>
        /// <param name="setNum">The set number (must be unique within this exercise).</param>
        /// <param name="weight">The weight used for the set in pounds.</param>
        /// <param name="reps">The number of repetitions performed.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when a set with the specified set number already exists.
        /// </exception>
        public void AddSet(int setNum, int weight, int reps)
        {
            if (Sets.Any(s => s.SetNumber == setNum))
                throw new ArgumentException($"Set number {setNum} already exists.");

            var set = new Set(setNum, weight, reps);
            Sets.Add(set);
        }

        /// <summary>
        /// Updates an existing set's weight and repetitions.
        /// </summary>
        /// <param name="setNum">The set number to update.</param>
        /// <param name="weight">The new weight in pounds.</param>
        /// <param name="reps">The new number of repetitions.</param>
        public void UpdateSet(int setNum, int weight, int reps)
        {
            var set = Sets.FirstOrDefault(s => s.SetNumber == setNum);
            if (set != null)
            {
                set.Weight = weight;
                set.Repetitions = reps;
            }
        }

        /// <summary>
        /// Removes a set from the exercise.
        /// </summary>
        /// <param name="setNum">The set number to remove.</param>
        public void RemoveSet(int setNum)
        {
            var set = Sets.FirstOrDefault(s => s.SetNumber == setNum);
            if (set != null)
            {
                Sets.Remove(set);
            }
        }

        /// <summary>
        /// Parameterless constructor for Entity Framework Core.
        /// </summary>
        private Exercise() { }
    }
}
