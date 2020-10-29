using HOHSI.Data;
using HOHSI.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HOHSI.Models.Repositories
{
    public class Repository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly HOHSIContext _context;

        //dependency injection for dbcontext
        public Repository(HOHSIContext context)
        {
            _context = context;
        }

        //just to save coding time
        protected async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Create(T entity)
        {
            _context.Add(entity);
            await Save();
        }

        public async Task Delete(T entity)
        {
            _context.Remove(entity);
            await Save();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await Save();
        }

        public Task<IEnumerable<T>> Find(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAll()
        {
            return _context.Set<T>().CountAsync();
        }

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().CountAsync(predicate);
        }
        public Task<bool> Any(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AnyAsync(predicate);
        }
    }
}

