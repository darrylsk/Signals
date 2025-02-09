using Signals.CoreLayer.Abstract.Base;
using Signals.InfrastructureLayer.Abstract;

namespace Signals.InfrastructureLayer.QuotationService.FinnhubQuotationService;

public interface IFinnhubQuotationService : 
    IQuotationService<FinnhubQuoteClientObject, FinnhubCompanyProfileClientObject>
{ }