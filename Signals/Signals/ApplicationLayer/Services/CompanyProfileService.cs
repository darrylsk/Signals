using System.Linq;
using System.Threading.Tasks;
using Signals.ApplicationLayer.Abstract;
using Signals.ApplicationLayer.Services.Base;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities;

namespace Signals.ApplicationLayer.Services;

public class CompanyProfileService : BusinessService<CompanyProfile>, ICompanyProfileService
{
    public ICompanyProfileRepository Repository { get; }

    public CompanyProfileService(ICompanyProfileRepository repository) : base(repository)
    {
        Repository = repository;
    }

    public async Task<CompanyProfile?> GetBySymbol(string symbol)
    {
        var companyProfile = (await Repository.GetAsync(x => x.Symbol == symbol)).FirstOrDefault();
        return companyProfile;
    }

    public async Task<int> Add(CompanyProfile model)
    {
        return await Repository.AddAsync(model); 

    }

    public async Task<int> Delete(CompanyProfile model)
    {
        return await Repository.DeleteAsync(model);
    }
}