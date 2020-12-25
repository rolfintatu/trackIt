using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrackIt.UI.Aggregates.ProjectAggregate;
using TrackIt.UI.Infrastructure;
using TrackIt.UI.Models;

namespace TrackIt.UI.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {

        private ProjectRepository _repo;
        private ApplicationUserManager _userManager;

        public ProjectController()
        {
            _repo = new ProjectRepository(new ProjectContext());
        }

        public ProjectController(ApplicationUserManager userManager) 
        {
            _repo = new ProjectRepository(new ProjectContext());
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        // GET: Project
        public async Task<ActionResult> Index()
        {
            var list = await _repo.GetListAsync();

            var returnList = list.Select(x =>
            {
                return new ProjectViewModel() { Id = x.Id, Name = x.ProjectName, Description = x.Description };
            });

            return View(returnList);
        }

        public ActionResult Create()
        {
            CreateProjectViewModel model = new CreateProjectViewModel();

            if(Session["workersList"] is null)
            {
                UserManager.Users.ToList().ForEach(x => {
                    model.Workers.Add(new ApplicationUserViewModel
                    {
                        Id = x.Id,
                        Name = x.UserName,
                        Image = null,
                        InProject = false
                    });
                });

                Session.Add("workersList", model.Workers);
            }
            else
            {
                model.Workers = (List<ApplicationUserViewModel>)Session["workersList"];
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateProjectViewModel model)
        {
            Project project = Project.CreateInstance(model.Name, model.Description);

            model.Workers.ForEach(x =>
            {
                project.AssignNewWorker(x.Id);
            });

            await _repo.Update(project);

            Session.Clear();

            return RedirectToAction("Index");
        }

        public PartialViewResult AddWorkerToProject(string workerId)
        {
            List<ApplicationUserViewModel> workerAdded = new List<ApplicationUserViewModel>();
            var newWorker = UserManager.Users.Where(x => x.Id == workerId).FirstOrDefault();

            var allWorkers = (List<ApplicationUserViewModel>)Session["workersList"];

            allWorkers.FirstOrDefault(x => x.Id == workerId).InProject = true;

            return PartialView("_UsersListPartialView", allWorkers);
        }


        [HttpGet]
        public ActionResult Details(Guid id)
        {
            ProjectDetailsViewModel model = new ProjectDetailsViewModel();

            Project project = _repo.GetById(id);

            model.Name = project.ProjectName;
            model.Description = project.Description;
            model.Workers = project.Workers.Select( x => {
                return UserManager.Users.FirstOrDefault(y => y.Id == x.WorkerId);
            } ).ToList();

            model.Tickets = project.Tickets.Select(x =>
            {
                return new TicketModel() { Name = x.Name, Description = x.Description };
            }).ToList();

            return View(model);
        }
    }
}