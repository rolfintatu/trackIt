using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackIt.UI.Aggregates.Enums;
using TrackIt.UI.Aggregates.Exceptions;
using static TrackIt.UI.Aggregates.Exceptions.WorkerAlreadyAssigned;
using static TrackIt.UI.Aggregates.Exceptions.WorkerIsNotAssign;

namespace TrackIt.UI.Aggregates.ProjectAggregate
{
    public class Project
    {
        protected Project() {
            _workers = new List<Worker>();
            _tickets = new List<Ticket>();
        }

        protected Project(Guid id, string projectName, string description)
        {
            Id = id;
            ProjectName = projectName;
            Description = description;
            _workers = new List<Worker>();
            _tickets = new List<Ticket>();
        }

        public Guid Id { get; protected set; }
        public string ProjectName { get; protected set; }
        public string Description { get; protected set; }


        private List<Ticket> _tickets;
        public  IList<Ticket> Tickets 
            => _tickets;

        private List<Worker> _workers;
        public IList<Worker> Workers
            => _workers;

        //Not persistent
        public bool workersChange { get; protected set; } = false;
        public bool ticketsChagne { get; protected set; } = false;


        public void CreateTicket(string name, string description, string assignTo)
        {
            if (_tickets is null)
                _tickets = new List<Ticket>();

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("");

            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("");

            _tickets.Add(new Ticket(this.Id, name, description, TicketState.New, assignTo));

            ticketsChagne = true;
        }

        public void CloseTicket(Guid ticketId)
        {
            Ticket ticket = _tickets.FirstOrDefault(x => x.Id.Equals(ticketId));

            if (ticket != null)
                ticket.ChangeTicketState(TicketState.Close);

            ticketsChagne = true;
        }

        public void AssignNewWorker(string workerId)
        {
            if (_workers is null)
                _workers = new List<Worker>();

            if (!_workers.Any(x => x.WorkerId.Equals(workerId)))
                _workers.Add(new Worker(workerId));
            else
                throw new WorkerAlreadyAssignedException();

            workersChange = true;
        }

        public void DismissWorker(string workerId)
        {

            Worker worker = _workers.FirstOrDefault(x => x.WorkerId == workerId);

            if (!(worker is null))
            {
                worker.DbState = DbState.Deleted;
            }
            else
                throw new WorkerIsNotAssignException();

            workersChange = true;
        }

        public void SetWorkers(List<Worker> newWorkers)
        {
            _workers.Clear();
            this._workers = newWorkers;
        }

        public static Project CreateInstance(string projectName, string description)
        {

            if (string.IsNullOrEmpty(projectName))
                throw new ArgumentNullException("");

            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("");

            return new Project(Guid.NewGuid() ,projectName, description);
        }
    }
}