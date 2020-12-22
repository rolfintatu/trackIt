using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackIt.UI.Models
{
    public class ProjectDetailsViewModel
    {

        public ProjectDetailsViewModel()
        {
            Workers = new List<ApplicationUser>();
            Tickets = new List<TicketModel>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<ApplicationUser> Workers { get; set; }
        public List<TicketModel> Tickets { get; set; }

    }


    public class TicketModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}