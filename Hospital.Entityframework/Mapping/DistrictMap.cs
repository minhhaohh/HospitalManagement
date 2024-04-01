using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Entityframework.Mapping
{
    public class DistrictMap : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.ToTable("Districts");

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

            builder.HasOne(x => x.Province)
                .WithMany(x => x.Districts)
                .HasForeignKey(x => x.ProvinceCode);

            builder.HasMany(x => x.Wards)
                .WithOne(x => x.District)
                .HasForeignKey(x => x.DistrictCode);
        }
    }
}
