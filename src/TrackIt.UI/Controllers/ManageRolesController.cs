using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrackIt.UI.AuthManagers;
using TrackIt.UI.Models;

namespace TrackIt.UI.Controllers
{
    [Authorize]
    public class ManageRolesController : Controller
    {
        private ApplicationUserManager UserManager;
        private ApplicationRoleManager RoleManager;


        public ManageRolesController() { }

        public ManageRolesController(ApplicationRoleManager roleManager, ApplicationUserManager userManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }


        // GET: ManageRoles
        public ActionResult Index(ManageRolesViewModel model)
        {
            model.AvailableUsers = UserManager.Users.ToList();

            string firstUserId;

            if (model.CurrentUserId != null)
                firstUserId = model.CurrentUserId;
            else
                firstUserId = model.AvailableUsers.First().Id;

            model.AvailableRoles = GetRoles(firstUserId);
            model.CurrentUserId = firstUserId;

            return View(model);
        }

        public PartialViewResult GetRolesFor(string Id)
        {
            return PartialView("_UserRolesPartial", new UserRolesViewModel() { Roles = GetRoles(Id), UserId = Id });
        }

        public async Task<ActionResult> AddRole(string roleName, string userId)
        {
            if(await RoleManager.RoleExistsAsync(roleName))
                await UserManager.AddToRoleAsync(userId, roleName);

            return RedirectToAction("Index", new ManageRolesViewModel() { CurrentUserId = userId });
        }

        public async Task<ActionResult> RemoveRole(string roleName, string userId)
        {
            await UserManager.RemoveFromRoleAsync(userId, roleName);

            return RedirectToAction("Index", new ManageRolesViewModel() { CurrentUserId = userId });
        }

        private Dictionary<string, bool> GetRoles(string UserId)
        {
            var rolesForUser = new Dictionary<string, bool>();
            var roles = RoleManager.Roles.ToList();
            var user = UserManager.Users.Where(x => x.Id == UserId).FirstOrDefault();

            roles.ForEach(x =>
            {
                if (user.Roles.Any(y => y.RoleId == x.Id))
                    rolesForUser.Add(x.Name, true);
                else
                    rolesForUser.Add(x.Name, false);
            });

            return rolesForUser;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (UserManager != null)
                {
                    UserManager.Dispose();
                    UserManager = null;
                }

                if (RoleManager != null)
                {
                    RoleManager.Dispose();
                    RoleManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}