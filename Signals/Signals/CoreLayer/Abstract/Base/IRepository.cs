using System.Threading.Tasks;
using Signals.CoreLayer.Entities.Base;

namespace Signals.CoreLayer.Abstract.Base;
public interface IRepository<T> : ISegregatedPartialRepository<T> where T : Entity
{
    Task<int> AddAsync(T entity);
    Task<int> DeleteAsync(T entity);
}