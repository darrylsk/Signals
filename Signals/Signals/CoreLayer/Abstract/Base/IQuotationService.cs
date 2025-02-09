using System.Threading.Tasks;

namespace Signals.CoreLayer.Abstract.Base;

public interface IQuotationService<T1,T2>
{
    /// <summary>
    /// The quotation service requires authentication via some kind of token in order to access it.
    /// </summary>
    public bool RequiresKey { get; set; }
    
    /// <summary>
    /// Retrieve the latest quote for a given ticker symbol.
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public Task<T1?> GetQuoteAsync(string symbol);
    
    /// <summary>
    /// Retrieve profile information about the company associated with the given ticker symbol.
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public Task<T2?> GetProfileAsync(string symbol);
}