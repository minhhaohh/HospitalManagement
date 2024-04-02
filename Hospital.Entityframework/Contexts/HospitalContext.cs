using Hospital.Entityframework.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Entityframework.Contexts
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.ApplyConfiguration(new ProvinceMap());
            modelBuilder.ApplyConfiguration(new DistrictMap());
            modelBuilder.ApplyConfiguration(new WardMap());
            modelBuilder.ApplyConfiguration(new PatientMap());
        }
    }
}
