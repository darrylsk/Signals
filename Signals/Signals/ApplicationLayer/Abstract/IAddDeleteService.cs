using System.Threading.Tasks;

namespace Signals.ApplicationLayer.Abstract;

public interface IAddDeleteService<T>
{
    Task<int> Add(T model);
    Task<int> Delete(T model);
}