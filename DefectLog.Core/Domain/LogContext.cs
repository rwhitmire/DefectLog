using System.Data.Entity;
using DefectLog.Core.Models;

namespace DefectLog.Core.Domain
{
    public class LogContext : DbContext
    {
        public DbSet<AppVersion> Versions { get; set; }
        public DbSet<Defect> Defects { get; set; }
        public DbSet<PriorityLevel> PriorityLevels { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Defect>()
                .HasOptional(x => x.Developer)
                .WithMany(x => x.DeveloperDefects);

            modelBuilder.Entity<Defect>()
                .HasOptional(x => x.Tester)
                .WithMany(x => x.TesterDefects);

            modelBuilder.Entity<Defect>()
                .HasOptional(x => x.Category)
                .WithMany(x => x.Defects)
                .WillCascadeOnDelete();
        }
    }
}