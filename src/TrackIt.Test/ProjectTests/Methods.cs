using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TrackIt.UI.Aggregates.Enums;
using TrackIt.UI.Aggregates.ProjectAggregate;

namespace TrackIt.Test.ProjectTests
{
    [TestClass]
    public class Methods
    {

        private Project fakeProject { get; set; }

        public Methods()
        {
            fakeProject = Project.CreateInstance("Test Project", "A fake project for testing.");
        }

        [TestMethod]
        public void add_ticket()
        {
            fakeProject.CreateTicket("First ticket", "A fake ticket for this project.", null);

            Assert.AreEqual(1, fakeProject.Tickets.Count());
        }

        [TestMethod]
        public void close_ticket()
        {
            fakeProject.CreateTicket("First ticket", "A fake ticket for this project.", null);

            Ticket fakeTicket = fakeProject.Tickets.First();

            fakeProject.CloseTicket(fakeTicket.Id);

            Assert.AreEqual(TicketState.Close, fakeTicket.TicketState);
        }

        [TestMethod]
        public void assign_new_worker()
        {
            fakeProject.AssignNewWorker(Guid.NewGuid().ToString());

            Assert.AreEqual(1, fakeProject.Workers.Count());
        }

        [TestMethod]
        public void dismiss_worker()
        {
            fakeProject.AssignNewWorker(Guid.NewGuid().ToString());

            Worker fakeWorker = fakeProject.Workers.First();

            fakeProject.DismissWorker(fakeWorker.WorkerId);

            Assert.AreEqual(0, fakeProject.Workers.Count());
        }

        [TestMethod]
        public void workers_changed()
        {
            fakeProject.AssignNewWorker(Guid.NewGuid().ToString());

            Assert.IsTrue(fakeProject.workersChange);
        }

        [TestMethod]
        public void tickets_changed()
        {
            fakeProject.CreateTicket("First ticket", "A fake ticket for this project.", null);

            Assert.IsTrue(fakeProject.ticketsChagne);
        }
    }
}
