using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalSynergyWebApi.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAllObj();

        Task<T> GetObjById(int Id);

        Task<T> UpsertObj(int Id, T obj);

        Task RemoveObj(T obj);

    }
}
