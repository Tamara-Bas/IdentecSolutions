using Microsoft.EntityFrameworkCore;

namespace IdentecSolutions.EF.Seed
{
    public interface IDataSeed
    {
        ModelBuilder Seed(ModelBuilder modelBuilder);
    }
}
