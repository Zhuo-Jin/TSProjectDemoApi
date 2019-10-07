using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalSynergyWebApi.Models;
using TotalSynergyWebApi.Models.DataModels;
using TotalSynergyWebApi.Models.Interfaces;



namespace TotalSynergyWebApi.Repository
{
    public class ProjectService : GenericRepository<Project>, IProjectService
    {

        private readonly ProjectDbContext _context;
        public ProjectService(ProjectDbContext context) : base (context)
        {
            _context = context;
        }

        public async Task<bool> AddRangeProjectContact(IEnumerable<IProjectContactItem> projectContactItems)
        {
            try
            {
                foreach (var pitem in projectContactItems)
                {
                    await _context.ProjectContactItems.AddAsync(pitem as ProjectContactItem);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        

        public async Task<bool> DeleteRangeProjectContact(int projectId)
        {
            try
            {
                var totalRemoveProjectContact = _context.ProjectContactItems.Where(pc => pc.ProjectId == projectId);

                

                foreach (var pitem in totalRemoveProjectContact)
                {
                    await Task.Run(() => _context.ProjectContactItems.Remove(pitem as ProjectContactItem));
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        
        public IEnumerable<IProject> GetAllProjects()
        {
            return _context.Projects.Include(p => p.ProjectContactItems).ThenInclude(pi => pi.Contact);
        }

        public async Task<IProject> GetProjectById(int Id) {
            return await _context.Projects.Include(p => p.ProjectContactItems).ThenInclude(pi => pi.Contact).FirstOrDefaultAsync(p => p.Id == Id);
        }


    }
}
