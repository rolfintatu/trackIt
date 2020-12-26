using System;
using TrackIt.UI.Aggregates.Enums;

namespace TrackIt.UI.Aggregates.ProjectAggregate
{
    public class Ticket
    {
        public Ticket() { this.Id = Guid.NewGuid(); }

        public Ticket(Guid projectId, string name, string description, TicketState ticketState, string assignTo)
            :this()
        {
            ProjectId = projectId;
            Name = name;
            Description = description;
            TicketState = ticketState;
            AssignTo = assignTo;
        }

        public Guid Id { get; protected set; }
        public Guid ProjectId { get; protected set; }
        public virtual Project Project { get; protected set; }

        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public TicketState TicketState { get; protected set; }

        public string AssignTo { get; protected set; }

        // Not persistent
        public DbState DbState { get; set; }


        public void ChangeTicketState(TicketState ticketState)
            => this.TicketState = ticketState;
    }
}