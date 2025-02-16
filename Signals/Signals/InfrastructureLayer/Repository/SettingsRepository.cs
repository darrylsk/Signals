using Signals.CoreLayer.Abstract.Base;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Signals.CoreLayer.Abstract;

namespace Signals.InfrastructureLayer.Repository;

public class SettingsRepository(ISignalsDbContext dbContext) : 
    SegregatedPartialRepository<Settings>(dbContext), ISettingsRepository
{

    public override async Task<IReadOnlyList<Settings>> GetAllAsync()
    {
        return await Context.Connection.Table<Settings>().ToListAsync();
    }

    public override async Task<IReadOnlyList<Settings>> GetAsync(Expression<Func<Settings, bool>> predicate)
    {
        return await Context.Connection.Table<Settings>().Where(predicate).ToListAsync();
    }

    public override async Task<Settings> GetByIdAsync(Guid id)
    {
        return await Context.Connection.Table<Settings>().FirstOrDefaultAsync(x => x.Id == id);
    }
}

