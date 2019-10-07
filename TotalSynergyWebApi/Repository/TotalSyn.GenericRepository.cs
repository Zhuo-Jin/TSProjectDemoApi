using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalSynergyWebApi.Models;

namespace TotalSynergyWebApi.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ProjectDbContext _context;
        public GenericRepository(ProjectDbContext context)
        {
            this._context = context;
        }
        public IEnumerable<T> GetAllObj()
        {
            return  _context.Set<T>().AsNoTracking();
        }

        public Task<T> GetObjById(int Id)
        {
            return _context.Set<T>().FindAsync(Id);
        }

        public async Task RemoveObj(T obj)
        {
            _context.Set<T>().Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<T> UpsertObj(int Id, T obj)
        {
            if (Id != 0)
            {
                _context.Entry(obj).State = EntityState.Modified;

            }
            else
            {
                _context.Set<T>().Add(obj);

            }

            try
            {
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
