using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackIt.UI.Models
{
    public class UserRolesViewModel
    {
        public Dictionary<string, bool> Roles { get; set; }
        public string UserId { get; set; }
    }
}