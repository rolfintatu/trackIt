using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackIt.UI.Models
{
    public class CreateProjectViewModel
    {

        public CreateProjectViewModel()
        {
            Workers = new List<ApplicationUserViewModel>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<ApplicationUserViewModel> Workers { get; set; }
    }
}