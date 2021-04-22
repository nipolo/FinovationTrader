using System.Threading.Tasks;

namespace Finovation.Core.Application.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>()
            where T : class;

        void SaveChanged();

        Task SaveChangedAsync();
    }
}
