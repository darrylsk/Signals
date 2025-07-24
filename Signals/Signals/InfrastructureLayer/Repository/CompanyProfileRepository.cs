using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MediatR;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.Repository.Base;

namespace Signals.InfrastructureLayer.Repository;

public class CompanyProfileRepository(ISignalsDbContext dbContext, IMediator mediator) 
    : Repository<CompanyProfile>(dbContext, mediator), ICompanyProfileRepository
{
    public override async Task<IReadOnlyList<CompanyProfile>> GetAllAsync()
    {
        return await Context.Connection.Table<CompanyProfile>().ToListAsync();

    }

    public override async Task<IReadOnlyList<CompanyProfile>> GetAsync(Expression<Func<CompanyProfile, bool>> predicate)
    {
        return await Context.Connection.Table<CompanyProfile>().Where(predicate).ToListAsync();
    }

    public override async Task<CompanyProfile> GetByIdAsync(Guid id)
    {
        return await Context.Connection.Table<CompanyProfile>().FirstOrDefaultAsync(x => x.Id == id);
    }
}