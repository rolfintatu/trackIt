using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackIt.UI.Models
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}