using IdentecSolutions.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Equipment> Equipments { get; set; }
    }
}
