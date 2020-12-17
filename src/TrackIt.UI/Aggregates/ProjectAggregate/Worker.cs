using System;

namespace TrackIt.UI.Aggregates.ProjectAggregate
{
    public class Worker
    {
        public Worker(string id)
        {
            Id = id;
        }

        public string Id { get; protected set; }
        public Guid ProjectId { get; protected set; }

        public string WorkerImage { get; protected set; }
    }
}