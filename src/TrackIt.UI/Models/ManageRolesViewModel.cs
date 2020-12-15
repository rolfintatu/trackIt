using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackIt.UI.Models
{
    public class ManageRolesViewModel
    {
        public List<ApplicationUser> AvailableUsers { get; set; }
        public Dictionary<string, bool> AvailableRoles  { get; set; }

        public string CurrentUserId { get; set; }
        public string RoleId { get; set; }
    }
}