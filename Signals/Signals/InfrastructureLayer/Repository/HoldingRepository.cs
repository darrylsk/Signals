using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Signals.CoreLayer.Abstract;
using Signals.Data;
using Signals.InfrastructureLayer.Repository.Base;

namespace Signals.InfrastructureLayer.Repository;

public class HoldingRepository(ISignalsDbContext dbContext)
    : Repository<Holding>(dbContext), IRepository<Holding>
{
    public override async Task<IReadOnlyList<Holding>> GetAllAsync()
    {
        return await Context.Connection.Table<Holding>().ToListAsync();
    }

    public override async Task<Holding> GetByIdAsync(Guid id)
    {
        return await Context.Connection.Table<Holding>().FirstOrDefaultAsync(x => x.Id == id);
    }
}