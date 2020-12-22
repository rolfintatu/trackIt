using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TrackIt.UI.Aggregates.ProjectAggregate;

namespace TrackIt.UI.Infrastructure
{
    public class ProjectContext : DbContext
    {

        public ProjectContext()
            :base("DefaultConnection") { }

        public DbSet<Project> Projects { get; set; }


    }
}