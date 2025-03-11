using Signals.CoreLayer.Entities;

namespace Signals.ApplicationLayer.Abstract;

public interface ISettingsService : IRetrieveService<Settings>, IUpdateService<Settings>
{
    
}