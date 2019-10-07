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
    public class ContactService : GenericRepository<Contact> , IContactService
    {
        private readonly ProjectDbContext _context;

        public ContactService(ProjectDbContext context) :  base(context)
        {
            _context = context;
        }

        public Task<string[]> GetProjectContactDependency(int ContactId)
        {
            return _context.ProjectContactItems.Include(p => p.Project).Where(p => p.ContactId == ContactId).Select(p => p.Project.ProjectName).ToArrayAsync<string>();
        }
    }
}
