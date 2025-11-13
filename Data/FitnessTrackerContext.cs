using Microsoft.EntityFrameworkCore;
using FitnessTracker.Models;

namespace FitnessTracker.Data
{
    public class FitnessTrackerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BodyMeasurement> BodyMeasurements { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<WorkoutSession> WorkoutSessions { get; set; }
        public DbSet<WeightEntry> WeightEntries { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<ExerciseCategory> ExerciseCategories { get; set; }

        public FitnessTrackerContext(DbContextOptions<FitnessTrackerContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=FitnessTracker;Trusted_Connection=True;");
            }
        }

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
        }
    }
}
