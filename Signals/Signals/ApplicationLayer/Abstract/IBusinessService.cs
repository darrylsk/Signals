using System.Threading.Tasks;

namespace Signals.ApplicationLayer.Abstract;

public interface IBusinessService<T> : ISegregatedPartialBusinessService<T>
{
    Task<int> Add(T model);
    Task<int> AddSymbol(string symbol);
    Task<int> Delete(T model);
}