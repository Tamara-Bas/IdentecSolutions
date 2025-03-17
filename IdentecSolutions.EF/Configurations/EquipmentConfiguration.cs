using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentecSolutions.EF.Configurations
{
    public class EquipmentConfiguration : IEntityTypeConfiguration<Domain.Entities.Equipment>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Equipment> builder)
        {
            builder.HasKey(e => e.Id); // Primary Key

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.SerialNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Price)
                .HasColumnType("decimal(18,2)"); // Precision for decimals

            builder.Property(e => e.WarrantyExpiryDate)
                 .HasColumnType("datetime2"); // Allows NULL

            builder.Property(e => e.Location)
              .IsRequired()
              .HasMaxLength(50);

            builder.Property(e => e.Status)
                .IsRequired();

            builder.Property(e => e.EquipmentType)
                .HasConversion<int>() // ✅ Converts Enum to int in DB
                .IsRequired();


            //// ✅ Audit Fields
            //builder.Property(e => e.AuditRecord.CreatedAt).HasColumnType("datetime2").IsRequired();
            //builder.Property(e => e.AuditRecord.CreatedBy).HasMaxLength(100).IsRequired();
            //builder.Property(e => e.AuditRecord.LastModifiedAt).HasColumnType("datetime2").IsRequired(false);
            //builder.Property(e => e.AuditRecord.LastModifiedBy).HasMaxLength(100).IsRequired(false);
        }
    }
}
