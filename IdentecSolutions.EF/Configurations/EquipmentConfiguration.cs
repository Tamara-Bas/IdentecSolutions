using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentecSolutions.EF.Configurations
{
    public class EquipmentConfiguration : IEntityTypeConfiguration<Domain.Entities.Equipment>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Equipment> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200);
        }
    }
}
