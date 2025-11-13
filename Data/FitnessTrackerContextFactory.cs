using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FitnessTracker.Data
{
    /// <summary>
    /// Factory for creating FitnessTrackerContext instances at design time for EF Core migrations.
    /// </summary>
    public class FitnessTrackerContextFactory : IDesignTimeDbContextFactory<FitnessTrackerContext>
    {
        public FitnessTrackerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FitnessTrackerContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=FitnessTracker;Trusted_Connection=True;");

            return new FitnessTrackerContext(optionsBuilder.Options);
        }
    }
}