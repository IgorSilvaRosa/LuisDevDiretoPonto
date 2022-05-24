namespace DevJobs.API.Persistence
{
    using  DevJobs.API.Entities;
    using Microsoft.EntityFrameworkCore;

    public class DevJobsContext : DbContext
    {
        public DevJobsContext(DbContextOptions<DevJobsContext> options) : base(options)
        {
            //JobVacancies = new List<JobVacancy>();
        }

        public DbSet<JobVacancy> JobVacancies {get; set;}
        public DbSet<JobApplication> JobApplications {get; set;}

        protected override void OnModelCreating(ModelBuilder builder){
            builder.Entity<JobVacancy>(e => {
                e.HasKey(jv => jv.Id);
                //e.ToTable("tb")
                e.HasMany(jv => jv.Applications)
                .WithOne()
                .HasForeignKey(ja => ja.IdJobVacancy)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<JobApplication>(e => {
                e.HasKey(ja => ja.Id);
            });
        }
    }
}