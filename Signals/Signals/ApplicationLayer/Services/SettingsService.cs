using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Signals.ApplicationLayer.Abstract;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities;

namespace Signals.ApplicationLayer.Services;

public class SettingsService : ISettingsService
{
    public SettingsService(ISettingsRepository repository)
    {
        Repository = repository;
    }

    public ISettingsRepository Repository { get; }

    public async Task<IEnumerable<Settings>> GetAll()
    {
        return await Repository.GetAllAsync();
    }

    public async Task<Settings> GetById(Guid id)
    {
        return await Repository.GetByIdAsync(id);
    }

    public async Task<int> Update(Settings model)
    {
        return await Repository.UpdateAsync(model);
    }
}