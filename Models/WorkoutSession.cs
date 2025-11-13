using System;
using System.Collections.Generic;

namespace FitnessTracker.Models
{
    /// <summary>
    /// Represents a workout session containing exercises performed on a specific date.
    /// </summary>
    public class WorkoutSession
    {
        /// <summary>
        /// Gets or sets the unique identifier for the workout session.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date when the workout session was performed.
        /// Cannot be in the future.
        /// </summary>
        public DateTime SessionDate { get; set; }

        /// <summary>
        /// Gets or sets the collection of exercises performed in this workout session.
        /// </summary>
        public List<Exercise> Exercises { get; set; } = [];

        /// <summary>
        /// Gets or sets the foreign key reference to the User entity.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the User that owns this workout session.
        /// Navigation property for Entity Framework.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkoutSession"/> class.
        /// </summary>
        /// <param name="sessionDate">The date of the workout session (cannot be in the future).</param>
        /// <param name="exercises">The list of exercises performed in this session.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when sessionDate is in the future.
        /// </exception>
        public WorkoutSession(DateTime sessionDate, List<Exercise> exercises)
        {
            if (sessionDate > DateTime.Today)
            {
                throw new ArgumentException("Session date cannot be in the future.", nameof(sessionDate));
            }

            SessionDate = sessionDate;
            Exercises = exercises;
        }

        /// <summary>
        /// Parameterless constructor for Entity Framework Core.
        /// </summary>
        private WorkoutSession() {}
    }
}
