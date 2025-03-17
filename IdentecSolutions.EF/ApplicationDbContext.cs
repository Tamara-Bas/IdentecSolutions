using IdentecSolutions.Domain.Entities;
using IdentecSolutions.EF.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Diagnostics;
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
            //Debugger.Launch();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            DataSeed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            //var context = eventData.Context;

            //if (context == null) return base.SaveChangesAsync( cancellationToken);
            var entries = ChangeTracker.Entries();

            foreach (var entry in ChangeTracker.Entries<IAudit>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return  base.SaveChanges();
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
