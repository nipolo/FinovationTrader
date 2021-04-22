using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Finovation.Core.Application.Abstractions
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity);

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity);

        TEntity GetById(Guid id);

        Task<TEntity> GetByIdAsync(Guid id);

        IQueryable<TEntity> QueryEntities();
    }
}
