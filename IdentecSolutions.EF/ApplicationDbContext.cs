using IdentecSolutions.Domain.Entities;
using IdentecSolutions.EF.Seed;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentecSolutions.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            DataSeed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void DataSeed(ModelBuilder modelBuilder)
        {
            var seedTypes = typeof(IDataSeed).Assembly.GetTypes()
                .Where(type => typeof(IDataSeed).IsAssignableFrom(type) && type.IsClass).ToList();

            foreach (var seedType in seedTypes)
            {
                var seedService = (IDataSeed)Activator.CreateInstance(seedType);
                seedService.Seed(modelBuilder);
            }
        }
        public DbSet<Equipment> Equipments { get; set; }
    }
}
