using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackIt.UI.Models
{
    public class CreateTicketViewModel
    {

        public CreateTicketViewModel()
        {
            AvailaibleWorkers = new List<ApplicationUserViewModel>();
        }

        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<ApplicationUserViewModel> AvailaibleWorkers { get; set; }

        public string Id { get; set; }
    }
}