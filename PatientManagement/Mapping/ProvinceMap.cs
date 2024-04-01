using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PatientManagement.Models;

namespace PatientManagement.Mapping
{
    public class ProvinceMap : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.ToTable("Provinces");

            builder.HasKey(x => x.Code);

            builder.Property(x => x.Code)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.LevelName)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(x => x.Districts)
                .WithOne(x => x.Province)
                .HasForeignKey(x => x.ProvinceCode);
        }
    }
}
