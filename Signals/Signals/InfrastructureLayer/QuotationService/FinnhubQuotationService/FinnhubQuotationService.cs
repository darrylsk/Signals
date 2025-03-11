using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Signals.CoreLayer.Abstract.Base;
using Signals.InfrastructureLayer.Abstract;

namespace Signals.InfrastructureLayer.QuotationService.FinnhubQuotationService;

public class FinnhubQuotationService : QuotationService<FinnhubQuoteClientObject?, FinnhubCompanyProfileClientObject?>,
    IFinnhubQuotationService
{
    public FinnhubQuotationService(ISignalsConfigurationService signalsConfigurationService) : base(signalsConfigurationService)
    {
        SuspensionTimeLimit = TimeSpan.FromDays(1);
        Uri = "https://finnhub.io/api/v1";
        Token = "";
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public override async Task<FinnhubQuoteClientObject?> GetQuoteAsync(string symbol)
    {
        var query = $"{Uri}/quote?symbol={symbol}&token={Token}";
        HttpResponseMessage response = await Client.GetAsync(query);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            if (string.IsNullOrEmpty(content.Replace("{}", "")) == false)
            {
                return GetQuoteClientData(content)!;
            }
        }

        if (response.StatusCode == (HttpStatusCode.TooManyRequests)) ServiceSuspended = true;
        
        return null;
    }

    private static FinnhubQuoteClientObject? GetQuoteClientData(string content)
    {
        var data = JsonSerializer.Deserialize<FinnhubQuoteClientObject>(content);
        return data;
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public override async Task<FinnhubCompanyProfileClientObject?> GetProfileAsync(string symbol)
    {
        if (string.IsNullOrEmpty(symbol) || string.IsNullOrEmpty(Token)) return null;
        var query = $"{Uri}/profile2?symbol={symbol}&token={Token}";
        HttpResponseMessage response = await Client.GetAsync(query);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            if (string.IsNullOrEmpty(content.Replace("{}", "")) == false)
            {
                return GetCompanyProfileClientData(content)!;
            }
        }

        if (response.StatusCode == (HttpStatusCode.TooManyRequests)) ServiceSuspended = true;
        
        return null;
    }

    private static FinnhubCompanyProfileClientObject? GetCompanyProfileClientData(string content)
    {
        var data = JsonSerializer.Deserialize<FinnhubCompanyProfileClientObject>(content);
        return data;
    }
}