using System.Threading.Tasks;
using Signals.CoreLayer.Entities;

namespace Signals.InfrastructureLayer.Abstract;

public interface IQuotationServiceAdapter
{
    /// <summary>
    /// Retrieve a quote from a selected quotation service
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    Task<WatchlistItem?> GetQuoteAsync(string symbol);
    
    /// <summary>
    /// Retrieve an organisation profile from a selected quotation service
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    Task<CompanyProfile?> GetProfileAsync(string symbol);
}