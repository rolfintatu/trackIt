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
    public class RoleController : Controller
    {
        private ApplicationRoleManager RoleManager;

        public RoleController() { }

        public RoleController(ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
        }


        // GET: Role
        public ActionResult Index()
        {
            List<RoleListViewModel> model = new List<RoleListViewModel>();

            model = RoleManager.Roles
                .Select<IdentityRole, RoleListViewModel>(
                    x => new RoleListViewModel() { Id = x.Id,  Name = x.Name }
                ).ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateRoleViewModel model)
        {
            var response = await RoleManager.CreateAsync(new IdentityRole(model.Name));

            if (response.Succeeded)
                return RedirectToAction("Index", "Role");
            else
                return View();
        }

        public async Task Delete(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            await RoleManager.DeleteAsync(role);
        }
    }
}