using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Entityframework.Mapping
{
    public class PatientMap : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(x => x.ChartNumber);

            builder.Property(x => x.ChartNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Gender)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Dob);

            builder.Property(x => x.Phone)
                .HasMaxLength(20);

            builder.Property(x => x.Email)
                .HasMaxLength(50);

            builder.Property(x => x.Address)
                .HasMaxLength(100);

            builder.HasOne(x => x.Ward)
                .WithMany()
                .HasForeignKey(x => x.WardCode);

            builder.HasOne(x => x.District)
                .WithMany()
                .HasForeignKey(x => x.DistrictCode);

            builder.HasOne(x => x.Province)
                .WithMany()
                .HasForeignKey(x => x.ProvinceCode);
        }
    }
}
