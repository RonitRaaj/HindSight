using Hindsight.Application.Interfaces;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Hindsight.Infrastructure.Services;

public class CurrencyExchange : ICurrencyExchange
{
    private readonly HttpClient _httpClient;

    public CurrencyExchange(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency, CancellationToken cancellationToken = default)
    {
        var source = fromCurrency.ToUpperInvariant().Trim();
        var target = toCurrency.ToUpperInvariant().Trim();

        if (source == target) return 1.0m;

        try
        {
            // Pulls dynamic real-time forex streams relative to the source fiat base currency
            var response = await _httpClient.GetFromJsonAsync<JsonNode>(
                $"https://open.er-api.com/v6/latest/{source}", 
                cancellationToken);

            var rates = response?["rates"];
            var rateValue = rates?[target]?.GetValue<decimal>();

            return rateValue ?? throw new InvalidOperationException($"Currency code '{target}' not supported by Forex API.");
        }
        catch (Exception ex)
        {
            // Resilient Fallbacks for 2026 baseline rates if the network drops out
            if (source == "INR" && target == "USD") return 0.012m;   // 1 INR ~ 0.012 USD
            if (source == "USD" && target == "INR") return 83.50m;   // 1 USD ~ 83.50 INR
            
            throw new InvalidOperationException($"Forex infrastructure failure mapping {source} to {target}: {ex.Message}", ex);
        }
    }
}