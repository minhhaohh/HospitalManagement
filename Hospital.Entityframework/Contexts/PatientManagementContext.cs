using Hospital.Entityframework.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Entityframework.Contexts
{
    public class PatientManagementContext : DbContext
    {
        public PatientManagementContext(DbContextOptions<PatientManagementContext> options) : base(options)
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
        }
    }
}
