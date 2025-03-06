using System;
using System.Net.Http;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Signals.InfrastructureLayer.Abstract;

namespace Signals.CoreLayer.Abstract.Base;

public abstract class QuotationService<T1, T2> : IQuotationService<T1, T2>, IDisposable
{
    public IConfigurationService ConfigurationService { get; }
    private bool _serviceSuspended;

    public QuotationService(IConfigurationService configurationService)
    {
        ConfigurationService = configurationService;
        Client = new HttpClient();
    }

    public HttpClient Client { get; }

    /// <summary>
    /// The uniform resource identifier of the remote quotation service provider.
    /// </summary>
    protected string Uri { get; set; }

    /// <summary>
    /// An indicator property to help detect when too many calls have been made to the quotation
    /// service within a time period.
    /// </summary>
    public bool ServiceSuspended
    {
        get
        {
            if (DateTime.Now - SuspensionTimeLimit < WhenSuspensionOccurred)
            {
                _serviceSuspended = false;
            }
            return _serviceSuspended;
        }

        set
        {
            WhenSuspensionOccurred = DateTime.Now;
            _serviceSuspended = value;
        }
    }

    public TimeSpan SuspensionTimeLimit { get; set; }
    public DateTime WhenSuspensionOccurred { get; set; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public bool RequiresKey { get; set; }

    protected string Token { get; set; }

    /// <summary>
    /// Composes and executes a REST based request for a quotation.
    /// </summary>
    public abstract Task<T1?> GetQuoteAsync(string symbol);
    
    /// <summary>
    /// Composes and executes a REST based request for company information.
    /// </summary>
    public abstract Task<T2?> GetProfileAsync(string symbol);

    public void Dispose()
    {
        Client.Dispose();
    }
}