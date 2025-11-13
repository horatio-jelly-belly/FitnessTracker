namespace FitnessTracker.Models
{
    /// <summary>
    /// Represents fitness goals for a user's training program.
    /// </summary>
    public enum FitnessGoal
    {
        WeightLoss,
        MuscleGain,
        Endurance,
        Strength,
        Maintenance,
        GeneralFitness
    }

    /// <summary>
    /// Represents the activity level of a user for calorie calculations.
    /// </summary>
    public enum ActivityLevel
    {
        Sedentary,        // Little/no exercise
        LightlyActive,    // 1-3 days/week
        ModeratelyActive, // 3-5 days/week
        VeryActive,       // 6-7 days/week
        ExtremelyActive   // Athlete/physical job
    }

    /// <summary>
    /// Represents a user's fitness profile with personal information, goals, and health metrics.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user profile.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user's activity level, used for calorie calculations.
        /// Default is Sedentary.
        /// </summary>
        public ActivityLevel ActivityLevel { get; set; } = ActivityLevel.Sedentary;

        /// <summary>
        /// Gets the height in feet. Must be greater than 0.
        /// </summary>
        public int HeightFeet { get; init; }

        /// <summary>
        /// Gets the height in inches. Must be 0-11.
        /// </summary>
        public int HeightInches { get; init; }

        /// <summary>
        /// Gets or sets the collection of weight entries for tracking weight history.
        /// </summary>
        public List<WeightEntry> WeightEntries { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of body measurements for tracking physical progress.
        /// </summary>
        public List<BodyMeasurement> BodyMeasurements { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of workout sessions for tracking exercise history.
        /// </summary>
        public List<WorkoutSession> WorkoutSessions { get; set; } = [];

        /// <summary>
        /// Gets the most recent weight entry, or null if no entries exist.
        /// </summary>
        public double? CurrentWeight => WeightEntries.Count > 0 ? WeightEntries[^1].Weight : null;

        /// <summary>
        /// Gets the user's date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; init; }

        /// <summary>
        /// Gets the user's current age calculated from date of birth.
        /// </summary>
        public int Age => CalculateAge();

        /// <summary>
        /// Gets or sets the user's fitness goal.
        /// </summary>
        public FitnessGoal Goal { get; set; }

        /// <summary>
        /// Gets or sets the daily calorie target based on BMR, activity level, and fitness goal.
        /// </summary>
        public int CalorieTarget { get; set; }

        /// <summary>
        /// Gets the user's gender. Must be "Male" or "Female".
        /// </summary>
        public string Gender { get; init; } = string.Empty;

        /// <summary>
        /// Gets the Body Mass Index calculated from current weight and height.
        /// Returns null if no weight entries exist.
        /// </summary>
        public double? CurrentBMI => CurrentWeight.HasValue ? CalculateBMI(CurrentWeight.Value) : null;

        /// <summary>
        /// Gets or sets the collection of meals associated with this user profile.
        /// </summary>
        public List<Meal> Meals { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="heightFeet">Height in feet (must be greater than 0).</param>
        /// <param name="heightInches">Height in inches (must be 0-11).</param>
        /// <param name="dateofBirth">Date of birth (must be in the past).</param>
        /// <param name="goal">The user's fitness goal.</param>
        /// <param name="gender">Gender ("Male" or "Female").</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when dateofBirth is not in the past or height values are invalid.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when gender is not "Male" or "Female".
        /// </exception>
        public User(int heightFeet, int heightInches, DateTime dateofBirth, FitnessGoal goal, string gender)
        {
            // Validate date, heightFeet, heightInches, and gender here because they are init-only
            if (dateofBirth >= DateTime.Today)
                throw new ArgumentOutOfRangeException(nameof(dateofBirth), "Date of birth must be in the past.");

            if (heightFeet <= 0 || heightInches < 0 || heightInches >= 12)
                throw new ArgumentOutOfRangeException("Height must be a valid feet and inches combination.");

            if (string.IsNullOrWhiteSpace(gender) ||
               (!string.Equals(gender, "female", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(gender, "male", StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("Gender must be either 'Male' or 'Female'.", nameof(gender));
            }

            HeightFeet = heightFeet;
            HeightInches = heightInches;
            DateOfBirth = dateofBirth;
            Goal = goal;
            Gender = gender;
        }

        /// <summary>
        /// Parameterless constructor for Entity Framework Core.
        /// </summary>
        private User() { } 

        /// <summary>
        /// Calculates the user's age based on date of birth.
        /// Accounts for whether the birthday has occurred this year.
        /// </summary>
        /// <returns>The user's age in years.</returns>
        private int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            // Subtract one year if birthday hasn't occurred yet this year
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }

        /// <summary>
        /// Updates the calorie target based on BMR, activity level, and fitness goal.
        /// Sets CalorieTarget to 0 if BMR cannot be calculated (no weight data).
        /// </summary>
        public void UpdateCalorieTarget()
        {
            double? bmr = CalculateBMR();

            if (bmr == null)
            {
                CalorieTarget = 0;
                return;
            }
                
            // Calculate Total Daily Energy Expenditure
            double tdee = bmr.Value * GetActivityMultiplier();

            // Adjust based on fitness goal
            CalorieTarget = Goal switch
            {
                FitnessGoal.WeightLoss => (int)(tdee - 500),      // 500 calorie deficit
                FitnessGoal.MuscleGain => (int)(tdee + 300),      // 300 calorie surplus
                FitnessGoal.Maintenance => (int)tdee,
                _ => (int)tdee
            };
        }

        /// <summary>
        /// Calculates Body Mass Index (BMI) for a given weight.
        /// Uses the formula: BMI = (weight / height²) × 703 (imperial units).
        /// </summary>
        /// <param name="weight">Weight in pounds.</param>
        /// <returns>The calculated BMI value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when weight is less than or equal to zero.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when height is invalid (zero or negative).
        /// </exception>
        public double CalculateBMI(double weight)
        {
            if (weight <= 0)
                throw new ArgumentOutOfRangeException(nameof(weight), "Weight must be greater than zero.");
            
            double heightInchesTotal = (HeightFeet * 12) + HeightInches;
            
            if (heightInchesTotal <= 0)
                throw new InvalidOperationException("Height must be greater than zero to calculate BMI.");
            
            return (weight / (heightInchesTotal * heightInchesTotal)) * 703;
        }

        /// <summary>
        /// Calculates Basal Metabolic Rate (BMR) using the Mifflin-St Jeor Formula.
        /// BMR represents the number of calories burned at rest.
        /// </summary>
        /// <returns>
        /// The calculated BMR in calories per day, or null if no weight data is available.
        /// </returns>
        public double? CalculateBMR()
        {
            // Need current weight in kg
            if (CurrentWeight == null) return null;

            double weightKg = CurrentWeight.Value * 0.453592; // Convert lbs to kg
            double heightCm = (HeightFeet * 12 + HeightInches) * 2.54; // Convert to cm

            // Mifflin-St Jeor Formula
            // Male: BMR = (10 × weight) + (6.25 × height) - (5 × age) + 5
            // Female: BMR = (10 × weight) + (6.25 × height) - (5 × age) - 161
            if (Gender.Equals("Male", StringComparison.OrdinalIgnoreCase))
            {
                return (10 * weightKg) + (6.25 * heightCm) - (5 * Age) + 5;
            }
            else // Female
            {
                return (10 * weightKg) + (6.25 * heightCm) - (5 * Age) - 161;
            }
        }

        /// <summary>
        /// Gets the activity multiplier based on the user's activity level.
        /// Used to calculate Total Daily Energy Expenditure (TDEE) from BMR.
        /// </summary>
        /// <returns>The activity multiplier (1.2 - 1.9).</returns>
        private double GetActivityMultiplier()
        {
            return ActivityLevel switch
            {
                ActivityLevel.Sedentary => 1.2,
                ActivityLevel.LightlyActive => 1.375,
                ActivityLevel.ModeratelyActive => 1.55,
                ActivityLevel.VeryActive => 1.725,
                ActivityLevel.ExtremelyActive => 1.9,
                _ => 1.2
            };
        }
    }
}
