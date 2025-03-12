using IdentecSolutions.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentecSolutions.EF.Seed
{
    public class EquipmentSeed : IDataSeed
    {
        public ModelBuilder Seed(ModelBuilder modelBuilder)
        {
            SeedEquipment(modelBuilder);

            return modelBuilder;
        }
        private static void SeedEquipment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment { Id = 1, Name = "Equipment 1"},
                new Equipment { Id = 2, Name = "Equipment 2" },
                new Equipment { Id = 3, Name = "Equipment 3" }
            );
        }
    }
}
