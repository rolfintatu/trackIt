using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TrackIt.UI.Aggregates.ProjectAggregate;

namespace TrackIt.UI.Infrastructure
{
    public class ProjectContext : DbContext
    {

        public ProjectContext()
            :base("DefaultConnection") { }

        public DbSet<Project> Projects { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>().Property(x => x.ProjectId).IsOptional();
            modelBuilder.Entity<Worker>().Ignore(x => x.DbState);
            modelBuilder.Entity<Project>().Ignore(x => x.workersChange).Ignore(x => x.ticketsChagne);
            modelBuilder.Entity<Ticket>().Ignore(x => x.DbState);
            base.OnModelCreating(modelBuilder);
        }
    }
}