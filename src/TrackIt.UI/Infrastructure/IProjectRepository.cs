using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackIt.UI.Aggregates.ProjectAggregate;
using TrackIt.UI.Models;

namespace TrackIt.UI.Infrastructure
{
    public interface IProjectRepository
    {
        Project GetById(Guid projectId);
        Task<List<Project>> GetListAsync();
        Task Update(Project obj);
    }
}