using Microsoft.EntityFrameworkCore;
using FitnessTracker.Models;

namespace FitnessTracker.Data
{
    /// <summary>
    /// Database context for the Fitness Tracker application.
    /// Provides access to all entity sets and configures entity relationships and seed data.
    /// </summary>
    public class FitnessTrackerContext : DbContext
    {
        /// <summary>
        /// Gets or sets the collection of user profiles.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the collection of body measurements for tracking physical progress.
        /// </summary>
        public DbSet<BodyMeasurement> BodyMeasurements { get; set; }

        /// <summary>
        /// Gets or sets the collection of meals for tracking nutrition.
        /// </summary>
        public DbSet<Meal> Meals { get; set; }

        /// <summary>
        /// Gets or sets the collection of workout sessions.
        /// </summary>
        public DbSet<WorkoutSession> WorkoutSessions { get; set; }

        /// <summary>
        /// Gets or sets the collection of weight entries for tracking weight history.
        /// </summary>
        public DbSet<WeightEntry> WeightEntries { get; set; }

        /// <summary>
        /// Gets or sets the collection of exercises performed during workout sessions.
        /// </summary>
        public DbSet<Exercise> Exercises { get; set; }

        /// <summary>
        /// Gets or sets the collection of sets performed for each exercise.
        /// </summary>
        public DbSet<Set> Sets { get; set; }

        /// <summary>
        /// Gets or sets the collection of food items associated with meals.
        /// </summary>
        public DbSet<FoodItem> FoodItems { get; set; }

        /// <summary>
        /// Gets or sets the collection of foods available in the nutrition database.
        /// </summary>
        public DbSet<Food> Foods { get; set; }

        /// <summary>
        /// Gets or sets the collection of exercise categories for organizing exercises.
        /// </summary>
        public DbSet<ExerciseCategory> ExerciseCategories { get; set; }

        /// <summary>
        /// Gets or sets the collection of exercise templates for predefined exercises.
        /// </summary>
        public DbSet<ExerciseTemplate> ExerciseTemplates { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FitnessTrackerContext"/> class.
        /// Uses default configuration.
        /// </summary>
        public FitnessTrackerContext() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FitnessTrackerContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public FitnessTrackerContext(DbContextOptions<FitnessTrackerContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Configures the database connection if not already configured.
        /// Uses SQL Server LocalDB by default.
        /// </summary>
        /// <param name="optionsBuilder">The builder used to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=FitnessTracker;Trusted_Connection=True;");
            }
        }

        /// <summary>
        /// Configures entity relationships and seeds initial data for foods, exercise categories, and exercise templates.
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.BodyMeasurements)
                .WithOne(bm => bm.User)
                .HasForeignKey(bm => bm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.WorkoutSessions)
                .WithOne(ws => ws.User)
                .HasForeignKey(ws => ws.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Meals)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.WeightEntries)
                .WithOne(we => we.User)
                .HasForeignKey(we => we.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutSession>()
                .HasMany(ws => ws.Exercises)
                .WithOne(e => e.WorkoutSession)
                .HasForeignKey(e => e.WorkoutSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Exercise>()
                .HasMany(e => e.Sets)
                .WithOne(s => s.Exercise)
                .HasForeignKey(s => s.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Meal>()
                .HasMany(m => m.FoodItems)
                .WithOne(fi => fi.Meal)
                .HasForeignKey(fi => fi.MealId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Exercise>()
                .HasOne(e => e.ExerciseCategory)
                .WithMany(ec => ec.Exercises)
                .HasForeignKey(e => e.ExerciseCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExerciseTemplate>()
                .HasOne(et => et.ExerciseCategory)
                .WithMany(ec => ec.ExerciseTemplates)
                .HasForeignKey(et => et.ExerciseCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Food>().HasData(
                new { Id = 1, FoodName = "Chicken Breast", ServingSize = 100.0, CaloriesPerServing = 165.0, ProteinPerServing = 31.0, CarbohydratesPerServing = 0.0, FatsPerServing = 3.6 },
                new { Id = 2, FoodName = "Salmon", ServingSize = 100.0, CaloriesPerServing = 208.0, ProteinPerServing = 20.0, CarbohydratesPerServing = 0.0, FatsPerServing = 13.0 },
                new { Id = 3, FoodName = "Ground Beef (90% lean)", ServingSize = 100.0, CaloriesPerServing = 176.0, ProteinPerServing = 20.0, CarbohydratesPerServing = 0.0, FatsPerServing = 10.0 },
                new { Id = 4, FoodName = "Eggs", ServingSize = 50.0, CaloriesPerServing = 78.0, ProteinPerServing = 6.0, CarbohydratesPerServing = 0.6, FatsPerServing = 5.0 },
                new { Id = 5, FoodName = "Tuna (canned)", ServingSize = 100.0, CaloriesPerServing = 116.0, ProteinPerServing = 26.0, CarbohydratesPerServing = 0.0, FatsPerServing = 1.0 },
                new { Id = 6, FoodName = "Turkey Breast", ServingSize = 100.0, CaloriesPerServing = 135.0, ProteinPerServing = 30.0, CarbohydratesPerServing = 0.0, FatsPerServing = 1.0 },

                new { Id = 7, FoodName = "Greek Yogurt", ServingSize = 150.0, CaloriesPerServing = 100.0, ProteinPerServing = 17.0, CarbohydratesPerServing = 6.0, FatsPerServing = 0.7 },
                new { Id = 8, FoodName = "Milk (2%)", ServingSize = 240.0, CaloriesPerServing = 122.0, ProteinPerServing = 8.0, CarbohydratesPerServing = 12.0, FatsPerServing = 5.0 },
                new { Id = 9, FoodName = "Cottage Cheese", ServingSize = 100.0, CaloriesPerServing = 98.0, ProteinPerServing = 11.0, CarbohydratesPerServing = 3.4, FatsPerServing = 4.3 },
                new { Id = 10, FoodName = "Cheddar Cheese", ServingSize = 28.0, CaloriesPerServing = 113.0, ProteinPerServing = 7.0, CarbohydratesPerServing = 0.4, FatsPerServing = 9.0 },

                new { Id = 11, FoodName = "Brown Rice (cooked)", ServingSize = 150.0, CaloriesPerServing = 165.0, ProteinPerServing = 3.5, CarbohydratesPerServing = 35.0, FatsPerServing = 1.5 },
                new { Id = 12, FoodName = "White Rice (cooked)", ServingSize = 150.0, CaloriesPerServing = 195.0, ProteinPerServing = 4.0, CarbohydratesPerServing = 45.0, FatsPerServing = 0.4 },
                new { Id = 13, FoodName = "Oatmeal (cooked)", ServingSize = 175.0, CaloriesPerServing = 158.0, ProteinPerServing = 6.0, CarbohydratesPerServing = 27.0, FatsPerServing = 3.0 },
                new { Id = 14, FoodName = "Whole Wheat Bread", ServingSize = 30.0, CaloriesPerServing = 81.0, ProteinPerServing = 4.0, CarbohydratesPerServing = 14.0, FatsPerServing = 1.0 },
                new { Id = 15, FoodName = "Pasta (cooked)", ServingSize = 150.0, CaloriesPerServing = 220.0, ProteinPerServing = 8.0, CarbohydratesPerServing = 43.0, FatsPerServing = 1.3 },
                new { Id = 16, FoodName = "Sweet Potato", ServingSize = 150.0, CaloriesPerServing = 129.0, ProteinPerServing = 2.0, CarbohydratesPerServing = 30.0, FatsPerServing = 0.1 },
                new { Id = 17, FoodName = "Quinoa (cooked)", ServingSize = 150.0, CaloriesPerServing = 180.0, ProteinPerServing = 6.0, CarbohydratesPerServing = 30.0, FatsPerServing = 2.5 },

                new { Id = 18, FoodName = "Broccoli", ServingSize = 100.0, CaloriesPerServing = 34.0, ProteinPerServing = 2.8, CarbohydratesPerServing = 7.0, FatsPerServing = 0.4 },
                new { Id = 19, FoodName = "Spinach", ServingSize = 100.0, CaloriesPerServing = 23.0, ProteinPerServing = 2.9, CarbohydratesPerServing = 3.6, FatsPerServing = 0.4 },
                new { Id = 20, FoodName = "Green Beans", ServingSize = 100.0, CaloriesPerServing = 31.0, ProteinPerServing = 1.8, CarbohydratesPerServing = 7.0, FatsPerServing = 0.1 },
                new { Id = 21, FoodName = "Carrots", ServingSize = 100.0, CaloriesPerServing = 41.0, ProteinPerServing = 0.9, CarbohydratesPerServing = 10.0, FatsPerServing = 0.2 },
                new { Id = 22, FoodName = "Bell Pepper", ServingSize = 100.0, CaloriesPerServing = 31.0, ProteinPerServing = 1.0, CarbohydratesPerServing = 6.0, FatsPerServing = 0.3 },

                new { Id = 23, FoodName = "Banana", ServingSize = 120.0, CaloriesPerServing = 105.0, ProteinPerServing = 1.3, CarbohydratesPerServing = 27.0, FatsPerServing = 0.4 },
                new { Id = 24, FoodName = "Apple", ServingSize = 150.0, CaloriesPerServing = 78.0, ProteinPerServing = 0.4, CarbohydratesPerServing = 21.0, FatsPerServing = 0.2 },
                new { Id = 25, FoodName = "Orange", ServingSize = 130.0, CaloriesPerServing = 62.0, ProteinPerServing = 1.2, CarbohydratesPerServing = 15.0, FatsPerServing = 0.2 },
                new { Id = 26, FoodName = "Blueberries", ServingSize = 100.0, CaloriesPerServing = 57.0, ProteinPerServing = 0.7, CarbohydratesPerServing = 14.0, FatsPerServing = 0.3 },
                new { Id = 27, FoodName = "Strawberries", ServingSize = 100.0, CaloriesPerServing = 32.0, ProteinPerServing = 0.7, CarbohydratesPerServing = 7.7, FatsPerServing = 0.3 },

                new { Id = 28, FoodName = "Almonds", ServingSize = 28.0, CaloriesPerServing = 164.0, ProteinPerServing = 6.0, CarbohydratesPerServing = 6.0, FatsPerServing = 14.0 },
                new { Id = 29, FoodName = "Peanut Butter", ServingSize = 32.0, CaloriesPerServing = 188.0, ProteinPerServing = 8.0, CarbohydratesPerServing = 6.0, FatsPerServing = 16.0 },
                new { Id = 30, FoodName = "Walnuts", ServingSize = 28.0, CaloriesPerServing = 185.0, ProteinPerServing = 4.3, CarbohydratesPerServing = 4.0, FatsPerServing = 18.0 }
            );

            // Exercise Categories
            modelBuilder.Entity<ExerciseCategory>().HasData(
                new { Id = 1, Name = "Chest", Description = "Exercises targeting the pectoral muscles" },
                new { Id = 2, Name = "Back", Description = "Exercises targeting the back muscles including lats, traps, and rhomboids" },
                new { Id = 3, Name = "Legs", Description = "Exercises targeting quadriceps, hamstrings, glutes, and calves" },
                new { Id = 4, Name = "Shoulders", Description = "Exercises targeting the deltoid muscles" },
                new { Id = 5, Name = "Arms", Description = "Exercises targeting biceps and triceps" },
                new { Id = 6, Name = "Core", Description = "Exercises targeting abdominals and obliques" },
                new { Id = 7, Name = "Cardio", Description = "Cardiovascular endurance exercises" }
            );

            // Exercise Templates
            modelBuilder.Entity<ExerciseTemplate>().HasData(
                // Chest (CategoryId = 1)
                new { Id = 1, Name = "Barbell Bench Press", ExerciseCategoryId = 1 },
                new { Id = 2, Name = "Incline Bench Press", ExerciseCategoryId = 1 },
                new { Id = 3, Name = "Decline Bench Press", ExerciseCategoryId = 1 },
                new { Id = 4, Name = "Dumbbell Bench Press", ExerciseCategoryId = 1 },
                new { Id = 5, Name = "Dumbbell Flys", ExerciseCategoryId = 1 },
                new { Id = 6, Name = "Cable Crossover", ExerciseCategoryId = 1 },
                new { Id = 7, Name = "Push-Ups", ExerciseCategoryId = 1 },
                new { Id = 8, Name = "Chest Dips", ExerciseCategoryId = 1 },
                new { Id = 9, Name = "Machine Chest Press", ExerciseCategoryId = 1 },
                new { Id = 10, Name = "Pec Deck Machine", ExerciseCategoryId = 1 },

                // Back (CategoryId = 2)
                new { Id = 11, Name = "Deadlift", ExerciseCategoryId = 2 },
                new { Id = 12, Name = "Barbell Row", ExerciseCategoryId = 2 },
                new { Id = 13, Name = "Dumbbell Row", ExerciseCategoryId = 2 },
                new { Id = 14, Name = "Lat Pulldown", ExerciseCategoryId = 2 },
                new { Id = 15, Name = "Pull-Ups", ExerciseCategoryId = 2 },
                new { Id = 16, Name = "Chin-Ups", ExerciseCategoryId = 2 },
                new { Id = 17, Name = "Seated Cable Row", ExerciseCategoryId = 2 },
                new { Id = 18, Name = "T-Bar Row", ExerciseCategoryId = 2 },
                new { Id = 19, Name = "Face Pulls", ExerciseCategoryId = 2 },
                new { Id = 20, Name = "Hyperextensions", ExerciseCategoryId = 2 },

                // Legs (CategoryId = 3)
                new { Id = 21, Name = "Barbell Squat", ExerciseCategoryId = 3 },
                new { Id = 22, Name = "Front Squat", ExerciseCategoryId = 3 },
                new { Id = 23, Name = "Leg Press", ExerciseCategoryId = 3 },
                new { Id = 24, Name = "Romanian Deadlift", ExerciseCategoryId = 3 },
                new { Id = 25, Name = "Leg Curl", ExerciseCategoryId = 3 },
                new { Id = 26, Name = "Leg Extension", ExerciseCategoryId = 3 },
                new { Id = 27, Name = "Walking Lunges", ExerciseCategoryId = 3 },
                new { Id = 28, Name = "Bulgarian Split Squat", ExerciseCategoryId = 3 },
                new { Id = 29, Name = "Calf Raises", ExerciseCategoryId = 3 },
                new { Id = 30, Name = "Goblet Squat", ExerciseCategoryId = 3 },
                new { Id = 31, Name = "Hip Thrust", ExerciseCategoryId = 3 },
                new { Id = 32, Name = "Hack Squat", ExerciseCategoryId = 3 },

                // Shoulders (CategoryId = 4)
                new { Id = 33, Name = "Overhead Press", ExerciseCategoryId = 4 },
                new { Id = 34, Name = "Dumbbell Shoulder Press", ExerciseCategoryId = 4 },
                new { Id = 35, Name = "Arnold Press", ExerciseCategoryId = 4 },
                new { Id = 36, Name = "Lateral Raises", ExerciseCategoryId = 4 },
                new { Id = 37, Name = "Front Raises", ExerciseCategoryId = 4 },
                new { Id = 38, Name = "Reverse Flys", ExerciseCategoryId = 4 },
                new { Id = 39, Name = "Upright Rows", ExerciseCategoryId = 4 },
                new { Id = 40, Name = "Shrugs", ExerciseCategoryId = 4 },
                new { Id = 41, Name = "Machine Shoulder Press", ExerciseCategoryId = 4 },

                // Arms (CategoryId = 5)
                new { Id = 42, Name = "Barbell Curl", ExerciseCategoryId = 5 },
                new { Id = 43, Name = "Dumbbell Curl", ExerciseCategoryId = 5 },
                new { Id = 44, Name = "Hammer Curl", ExerciseCategoryId = 5 },
                new { Id = 45, Name = "Preacher Curl", ExerciseCategoryId = 5 },
                new { Id = 46, Name = "Concentration Curl", ExerciseCategoryId = 5 },
                new { Id = 47, Name = "Cable Curl", ExerciseCategoryId = 5 },
                new { Id = 48, Name = "Tricep Pushdown", ExerciseCategoryId = 5 },
                new { Id = 49, Name = "Skull Crushers", ExerciseCategoryId = 5 },
                new { Id = 50, Name = "Overhead Tricep Extension", ExerciseCategoryId = 5 },
                new { Id = 51, Name = "Tricep Dips", ExerciseCategoryId = 5 },
                new { Id = 52, Name = "Close-Grip Bench Press", ExerciseCategoryId = 5 },
                new { Id = 53, Name = "Diamond Push-Ups", ExerciseCategoryId = 5 },

                // Core (CategoryId = 6)
                new { Id = 54, Name = "Crunches", ExerciseCategoryId = 6 },
                new { Id = 55, Name = "Plank", ExerciseCategoryId = 6 },
                new { Id = 56, Name = "Russian Twists", ExerciseCategoryId = 6 },
                new { Id = 57, Name = "Leg Raises", ExerciseCategoryId = 6 },
                new { Id = 58, Name = "Mountain Climbers", ExerciseCategoryId = 6 },
                new { Id = 59, Name = "Bicycle Crunches", ExerciseCategoryId = 6 },
                new { Id = 60, Name = "Dead Bug", ExerciseCategoryId = 6 },
                new { Id = 61, Name = "Ab Rollout", ExerciseCategoryId = 6 },
                new { Id = 62, Name = "Cable Woodchop", ExerciseCategoryId = 6 },
                new { Id = 63, Name = "Hanging Knee Raises", ExerciseCategoryId = 6 },

                // Cardio (CategoryId = 7)
                new { Id = 64, Name = "Treadmill Running", ExerciseCategoryId = 7 },
                new { Id = 65, Name = "Stationary Bike", ExerciseCategoryId = 7 },
                new { Id = 66, Name = "Elliptical", ExerciseCategoryId = 7 },
                new { Id = 67, Name = "Rowing Machine", ExerciseCategoryId = 7 },
                new { Id = 68, Name = "Stair Climber", ExerciseCategoryId = 7 },
                new { Id = 69, Name = "Jump Rope", ExerciseCategoryId = 7 },
                new { Id = 70, Name = "Burpees", ExerciseCategoryId = 7 },
                new { Id = 71, Name = "Box Jumps", ExerciseCategoryId = 7 },
                new { Id = 72, Name = "Kettlebell Swings", ExerciseCategoryId = 7 }
            );
        }
    }
}
