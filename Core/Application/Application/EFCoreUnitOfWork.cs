using System.Threading.Tasks;

using Finovation.Core.Application.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace Finovation.Core.Application
{
    public class EFCoreUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public EFCoreUnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<T> GetRepository<T>()
            where T : class
        {
            return new EFCoreRepository<T>(_dbContext);
        }

        public void SaveChanged()
        {
            _dbContext.SaveChanges();
            _dbContext.ChangeTracker.Clear();
        }

        public async Task SaveChangedAsync()
        {
            await _dbContext.SaveChangesAsync();
            _dbContext.ChangeTracker.Clear();
        }
    }
}
