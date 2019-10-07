using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalSynergyWebApi.Models.DataModels;
using TotalSynergyWebApi.Models.Interfaces;

namespace TotalSynergyWebApi.Repository
{
    public interface IProjectService : IGenericRepository<Project>
    {
        IEnumerable<IProject> GetAllProjects();
        Task<IProject> GetProjectById(int Id);

        Task<bool> AddRangeProjectContact(IEnumerable<IProjectContactItem> projectContactItems);

        Task<bool> DeleteRangeProjectContact(int projectId);

    }
}
