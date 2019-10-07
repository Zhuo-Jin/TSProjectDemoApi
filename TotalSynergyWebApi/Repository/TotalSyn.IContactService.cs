using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalSynergyWebApi.Models.DataModels;
using TotalSynergyWebApi.Models.Interfaces;

namespace TotalSynergyWebApi.Repository
{
    public interface IContactService : IGenericRepository<Contact>
    {
        Task<string[]> GetProjectContactDependency(int ContactId);
    }
}
