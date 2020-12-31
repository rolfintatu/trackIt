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

            model.Workers = model.Workers.OrderBy(x => x.InProject == false).ToList();

            return View(model);
        }

        public ActionResult Ticket(Guid ProjectId, Guid TicketId)
        {
            var project = _repo.GetById(ProjectId);
            var ticket = project.Tickets.Where(x => x.Id == TicketId).FirstOrDefault();

            var model = new TicketDetailsViewModel
            {
                Id = ticket.Id,
                Name = ticket.Name,
                Description = ticket.Description,
                Status = ticket.TicketState.ToString(),
                AssingTo = ticket.AssignTo
            };

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

        public ActionResult CreateTicket(Guid projectId)
        {
            CreateTicketViewModel model = new CreateTicketViewModel();

            if(Session["availableWorkers"] is null)
            {
                var availableWorkers = _repo.GetById(projectId).Workers;

                model.AvailaibleWorkers = availableWorkers.Select(x => {
                    return new ApplicationUserViewModel() { Id = x.WorkerId.ToString(), Image = null, InProject = false, Name = null };
                }).ToList();

                Session["availableWorkers"] = model.AvailaibleWorkers;
            }
            else
            {
                model.AvailaibleWorkers = (List<ApplicationUserViewModel>)Session["availableWorkers"];
            }

            model.ProjectId = projectId;
            model.AvailaibleWorkers = model.AvailaibleWorkers.OrderBy(x => x.InProject == false).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTicket(CreateTicketViewModel model)
        {
            var project = _repo.GetById(model.ProjectId);

            project.CreateTicket(model.Name, model.Description, model.AvailaibleWorkers.First().Id);

            await _repo.Update(project);

            Session.Clear();

            return RedirectToAction($"Details/{ model.ProjectId }");
        }

        public PartialViewResult AssignWorkerToTicket(string workerId, string projectId)
        {

            var workers = (List<ApplicationUserViewModel>)Session["availableWorkers"];

            if(workers.Any(x => x.InProject == true && x.Id != workerId))
            {
                workers.Where(x => x.InProject == true && x.Id != workerId).ToList().ForEach(y => y.InProject = false);
                workers.FirstOrDefault(x => x.Id == workerId).InProject = true;
            }
            else
            {
                workers.FirstOrDefault(x => x.Id == workerId).InProject = true;
            }


            var returnModel = new Dictionary<Guid, List<ApplicationUserViewModel>>();
            returnModel.Add(Guid.Parse(projectId), workers.ToList());

            return PartialView("_WorkersListForTicket", returnModel);
        }

        public PartialViewResult DismissWorkerFromTicket(string workerId, string projectId)
        {
            var workers = (List<ApplicationUserViewModel>)Session["availableWorkers"];

            if (workers.Any(x => x.Id == workerId))
                workers.FirstOrDefault(x => x.Id == workerId).InProject = false;

            var returnModel = new Dictionary<Guid, List<ApplicationUserViewModel>>();
            returnModel.Add(Guid.Parse(projectId), workers.ToList());
            return PartialView("_WorkersListForTicket", returnModel);
        }

        public PartialViewResult AddWorkerToProject(string workerId)
        {
            List<ApplicationUserViewModel> workerAdded = new List<ApplicationUserViewModel>();
            var newWorker = UserManager.Users.Where(x => x.Id == workerId).FirstOrDefault();

            var allWorkers = (List<ApplicationUserViewModel>)Session["workersList"];

            allWorkers.FirstOrDefault(x => x.Id == workerId).InProject = true;

            return PartialView("_UsersListPartialView", allWorkers);
        }

        public PartialViewResult RemoveWorkerFromProject(string workerId)
        {
            List<ApplicationUserViewModel> workerAdded = new List<ApplicationUserViewModel>();
            var newWorker = UserManager.Users.Where(x => x.Id == workerId).FirstOrDefault();

            var allWorkers = (List<ApplicationUserViewModel>)Session["workersList"];
            var worker = allWorkers.FirstOrDefault(x => x.Id == workerId);

            if (worker.InProject == true)
                worker.InProject = false;

            return PartialView("_UsersListPartialView", allWorkers);
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            ProjectDetailsViewModel model = new ProjectDetailsViewModel();

            Project project = _repo.GetById(id);

            model.Name = project.ProjectName;
            model.Description = project.Description;
            model.ProjectId = project.Id;

            model.Workers = project.Workers.Select( x => {
                return UserManager.Users.FirstOrDefault(y => y.Id == x.WorkerId);
            } ).ToList();

            model.Tickets = project.Tickets.Select(x =>
            {
                return new TicketModel() { Name = x.Name, Description = x.Description, Id = x.Id };
            }).ToList();

            return View(model);
        }

        public PartialViewResult GetAllWorkers(List<ApplicationUser> workersInProject, Guid projectId)
        {
            var model = new List<ApplicationUserViewModel>();

            model = UserManager.Users.ToList().Select(x =>
            {
                return new ApplicationUserViewModel { 
                    Id = x.Id, 
                    InProject = workersInProject.Any(y => y.Id == x.Id) ? true : false, 
                    Name = x.UserName 
                };
            }).ToList();

            var result = new Dictionary<Guid, List<ApplicationUserViewModel>>();
            result.Add(projectId, model);

            return PartialView("_ProjectWorkersParialView", result);
        }

        public async Task<PartialViewResult> AddNewWorkerToProject(string workerId, string projectId)
        {
            Guid projectIdGuid = Guid.Parse(projectId);
            var project = _repo.GetById(projectIdGuid);
            project.AssignNewWorker(workerId);
            await _repo.Update(project);

            Details(projectIdGuid);

            var result = project.Workers.Select(x =>
            {
                return new ApplicationUserViewModel { Id = x.Id.ToString(), Name = null, InProject = true };
            }).ToList();

            var resultList = new Dictionary<Guid, List<ApplicationUserViewModel>>();
            resultList.Add(Guid.Empty, result);

            return PartialView("_ProjectWorkersParialView", resultList);
        }

        public async Task<PartialViewResult> DismissWorkerFromProject(string workerId, string projectId)
        {
            try
            {
                Guid projectIdGuid = Guid.Parse(projectId);
                var project = _repo.GetById(projectIdGuid);
                project.DismissWorker(workerId);
                await _repo.Update(project);

                var result = project.Workers.Select(x =>
                {
                    return new ApplicationUserViewModel { Id = x.Id.ToString(), Name = null, InProject = true };
                }).ToList();

                var resultList = new Dictionary<Guid, List<ApplicationUserViewModel>>();
                resultList.Add(Guid.Empty, result);

                return PartialView("_ProjectWorkersParialView", resultList);
            }
            catch(Exception ex)
            {
                throw;
            }

        }
    }
}