using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Finovation.Core.Application.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace Finovation.Core.Application
{
    public class EFCoreRepository<T> : IRepository<T>
        where T : class
    {
        private readonly DbContext _dbContext;

        public EFCoreRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Add(entity);
        }

        public Task AddAsync(T entity)
        {
            return _dbContext.AddAsync(entity).AsTask();
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public T GetById(Guid id)
        {
            return _dbContext.Find<T>(id);
        }
        public Task<T> GetByIdAsync(Guid id)
        {
            return _dbContext.FindAsync<T>(id).AsTask();
        }

        public IQueryable<T> QueryEntities()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public void Update(T entity)
        {
            _dbContext.Update<T>(entity);
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
