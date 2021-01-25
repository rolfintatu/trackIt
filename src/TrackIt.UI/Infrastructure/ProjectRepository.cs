using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TrackIt.UI.Aggregates.ProjectAggregate;
using TrackIt.UI.AuthManagers;
using TrackIt.UI.Models;

namespace TrackIt.UI.Infrastructure
{
    public class ProjectRepository : IProjectRepository
    {
        private ProjectContext _context;
        private ApplicationUserManager _userManager;

        public ProjectRepository(ProjectContext context, ApplicationUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task Update(Project obj)
        {
            if (!_context.Projects.Any(x => x.Id.Equals(obj.Id)))
            {
                _context.Projects.Add(obj);
                await _context.SaveChangesAsync();
            }
            else
            {
                try
                {
                    _context.Entry(obj).State = System.Data.Entity.EntityState.Modified;

                    if (obj.ticketsChagne)
                    {
                        foreach (var ticket in obj.Tickets)
                        {
                            switch (ticket.DbState)
                            {
                                case Aggregates.Enums.DbState.Added:
                                    _context.Entry(ticket).State = System.Data.Entity.EntityState.Added;
                                    break;
                                case Aggregates.Enums.DbState.Deleted:
                                    _context.Entry(ticket).State = System.Data.Entity.EntityState.Deleted;
                                    break;
                                case Aggregates.Enums.DbState.Modified:
                                    _context.Entry(ticket).State = System.Data.Entity.EntityState.Modified;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    if (obj.workersChange)
                    {
                        for (int i = 0; i <= obj.Workers.Count - 1; i++)
                        {
                            switch (obj.Workers[i].DbState)
                            {
                                case Aggregates.Enums.DbState.Added:
                                    _context.Entry(obj.Workers[i]).State = System.Data.Entity.EntityState.Added;
                                    break;
                                case Aggregates.Enums.DbState.Deleted:
                                    _context.Entry(obj.Workers[i]).State = System.Data.Entity.EntityState.Deleted;
                                    break;
                                case Aggregates.Enums.DbState.Modified:
                                    _context.Entry(obj.Workers[i]).State = System.Data.Entity.EntityState.Modified;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }

        public Task<List<Project>> GetListAsync()
        {
            List<Project> projects = _context.Projects
                .ToList();

            return Task.FromResult(projects);
        }

        public Project GetById(Guid projectId)
        {
            var project = _context.Projects.Where(x => x.Id == projectId)
                .Include(x => x.Workers)
                .Include(x => x.Tickets)
                .FirstOrDefault();

            var applicationUsers = _userManager.Users.ToList();

            var workers = project.Workers.Join(applicationUsers, 
                w => w.WorkerId, 
                u => u.Id, 
                (w, u) => { 
                        w.SetUserName(u.UserName); return w; 
                    }
                );

            project.SetWorkers(workers.ToList());

            return project;
        }

    }
}