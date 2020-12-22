using System;
using TrackIt.UI.Aggregates.Enums;

namespace TrackIt.UI.Aggregates.ProjectAggregate
{
    public class Worker
    {
        public Worker(string id)
        {
            Id = id;
        }

        public Worker()
        {

        }

        public string Id { get; protected set; }
        public Guid ProjectId { get; protected set; }
        public virtual Project Project { get; protected set; }


        public string WorkerImage { get; protected set; }


        // Not persistent
        public DbState DbState { get; set; }
    }
}