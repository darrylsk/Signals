using System.Threading.Tasks;

namespace Signals.ApplicationLayer.Abstract;

public interface IUpdateService<T>
{
    Task<int> Update(T model);
}