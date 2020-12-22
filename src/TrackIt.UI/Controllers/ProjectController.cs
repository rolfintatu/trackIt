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

            List<string> tempModel = 
                (List<string>)TempData["newModel"];

            UserManager.Users.ToList().ForEach(x => {
                model.Workers.Add(x, false);
            });

            if (tempModel is null || tempModel.Count() == 0)
            {
                return View(model);
            }
            else
            {
                CreateProjectViewModel newModel = new CreateProjectViewModel();
                newModel.Name = model.Name;
                newModel.Description = model.Description;
                newModel.Workers = new Dictionary<ApplicationUser, bool>();

                model.Workers.ToList().ForEach(x =>
                {
                    tempModel.ForEach(y =>
                    {
                        if (x.Key.Id == y)
                            newModel.Workers.Add(x.Key, true);
                        else
                            newModel.Workers.Add(x.Key, false);
                    });
                });

                return View(newModel);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateProjectViewModel model)
        {
            Project project = Project.CreateInstance(model.Name, model.Description);

            var workers = (List<string>)TempData["newModel"];

            workers.ForEach(x =>
            {
                project.AssignNewWorker(x);
            });

            await _repo.Update(project);

            return View();
        }

        public void AddWorkerToProject(string workerId)
        {
            List<string> workerAdded = new List<string>();
            workerAdded.Add(workerId);

            TempData["newModel"] = workerAdded;
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            ProjectDetailsViewModel model = new ProjectDetailsViewModel();

            Project project = _repo.GetById(id);



            return View(model);
        }
    }
}