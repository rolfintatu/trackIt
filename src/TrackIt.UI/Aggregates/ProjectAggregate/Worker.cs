using System;
using TrackIt.UI.Aggregates.Enums;

namespace TrackIt.UI.Aggregates.ProjectAggregate
{
    public class Worker
    {
        public Worker(string workerId)
        {
            this.Id = Guid.NewGuid();
            WorkerId = workerId;
        }

        public Worker()
        {

        }

        public Guid Id { get; protected set; }
        public Guid ProjectId { get; protected set; }
        public string WorkerId { get; protected set; }
        public virtual Project Project { get; protected set; }


        public string WorkerImage { get; protected set; }


        // Not persistent
        public DbState DbState { get; set; }
    }
}