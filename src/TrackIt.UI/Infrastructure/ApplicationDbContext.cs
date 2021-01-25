using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using TrackIt.UI.Aggregates.ProjectAggregate;
using TrackIt.UI.AuthModels;

namespace TrackIt.UI.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        { }

        DbSet<Project> Projects { get; set; }
        DbSet<Worker> Workers { get; set; }
        DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>().Property(x => x.ProjectId).IsOptional();
            modelBuilder.Entity<Worker>().Ignore(x => x.DbState);
            modelBuilder.Entity<Worker>().Ignore(x => x.UserName);
            modelBuilder.Entity<Project>().Ignore(x => x.workersChange).Ignore(x => x.ticketsChagne);
            modelBuilder.Entity<Ticket>().Ignore(x => x.DbState);
            base.OnModelCreating(modelBuilder);
        }
    }
}