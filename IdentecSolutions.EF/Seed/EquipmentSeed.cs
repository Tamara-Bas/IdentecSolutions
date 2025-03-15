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
                new Equipment { 
                    Id = 1, 
                    Name = "Equipment 1", 
                    Description="Equipment one",
                    SerialNumber="123",
                    Price=100,
                    IsInUsage=true, 
                    WarrantyExpiryDate=DateTime.Now, 
                    Location="Shop",  
                    Status = true
    },
                new Equipment { Id = 2, 
                    Name = "Equipment 2",
                    Description = "Equipment two",
                    SerialNumber = "1234",
                    Price = 100,
                    IsInUsage = true,
                    WarrantyExpiryDate = DateTime.Now,
                    Location = "Store",
                    Status = true
                },
                new Equipment { Id = 3, 
                    Name = "Equipment 3",
                    Description = "Equipment three",
                    SerialNumber = "123",
                    Price = 100,
                    IsInUsage = true,
                    WarrantyExpiryDate =  DateTime.Now,
                    Location = "Factory",
                    Status = true
                }
            );
        }
    }
}
