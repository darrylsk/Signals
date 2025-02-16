using Signals.ApplicationLayer.Abstract;
using Signals.ApplicationLayer.Services.Base;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Abstract.Base;
using Signals.CoreLayer.Entities;

namespace Signals.ApplicationLayer.Services;

public class SettingsService(ISettingsRepository repository)
    : SegregatedPartialBusinessService<Settings>(repository)
{
}