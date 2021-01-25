using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackIt.UI.AuthModels;

namespace TrackIt.UI.Models
{
    public class ProjectDetailsViewModel
    {

        public ProjectDetailsViewModel()
        {
            Workers = new List<ApplicationUser>();
            Tickets = new List<TicketModel>();
        }

        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<ApplicationUser> Workers { get; set; }
        public List<TicketModel> Tickets { get; set; }

    }


    public class TicketModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}