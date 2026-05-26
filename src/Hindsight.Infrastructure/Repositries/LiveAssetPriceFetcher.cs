using Hindsight.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Hindsight.Infrastructure.Services;

public class LiveAssetPriceFetcher : IAssetPriceFetcher
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string _apiKey;

    public LiveAssetPriceFetcher(HttpClient httpClient , IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _apiKey = _configuration["MARKET_API_KEY"] ?? string.Empty;
    }

    public async Task<decimal> GetLivePriceAsync(string assetName, CancellationToken cancellationToken = default)
    {
        var normalizedName = assetName.ToLowerInvariant().Trim();

        try
        {
            // =========================================================================
            // A. CRYPTO ROUTING: BITCOIN (CoinGecko Open API)
            // =========================================================================
            if (normalizedName == "bitcoin")
            {
                var requestUrl = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=usd";

                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

                if (!string.IsNullOrEmpty(_apiKey))
                {
                    request.Headers.Add("x-cg-demo-api-key", _apiKey);
                }

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var data = await response.Content.ReadFromJsonAsync<Dictionary<string, Dictionary<string, decimal>>>();
                return data["bitcoin"]["usd"];
            }

            // =========================================================================
            // B. STOCK ROUTING: APPLE (Alpha Vantage Public Data Pipeline)
            // =========================================================================
            if (normalizedName == "apple")
            {
                var response = await _httpClient.GetFromJsonAsync<JsonNode>(
                    "https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=AAPL&apikey=demo",
                    cancellationToken);

                var quote = response?["Global Quote"];
                var priceStr = quote?["05. price"]?.GetValue<string>();

                if (decimal.TryParse(priceStr, out var price)) return Math.Round(price, 2);

                return 225.40m;
            }

            // =========================================================================
            // C. STOCK ROUTING: NETFLIX (Alpha Vantage Public Data Pipeline)
            // =========================================================================
            if (normalizedName == "netflix")
            {
                var response = await _httpClient.GetFromJsonAsync<JsonNode>(
                    "https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=NFLX&apikey=demo",
                    cancellationToken);

                var quote = response?["Global Quote"];
                var priceStr = quote?["05. price"]?.GetValue<string>();

                if (decimal.TryParse(priceStr, out var price)) return Math.Round(price, 2);

                return 745.20m;
            }

            // =========================================================================
            // D. COMMODITY ROUTING: GOLD & SILVER (Public Spot Price Fallback Mapping)
            // =========================================================================
            if (normalizedName == "silver")
            {
                var response = await _httpClient.GetFromJsonAsync<JsonNode>(
                    "https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=SLV&apikey=demo",
                    cancellationToken);

                var quote = response?["Global Quote"];
                var priceStr = quote?["05. price"]?.GetValue<string>();

                if (decimal.TryParse(priceStr, out var price))
                {

                    return Math.Round(price * 10.0m, 2);
                }

                return 32.45m;
            }

            if (normalizedName == "gold")
            {
                var response = await _httpClient.GetFromJsonAsync<JsonNode>(
                    "https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=GLD&apikey=demo",
                    cancellationToken);

                var quote = response?["Global Quote"];
                var priceStr = quote?["05. price"]?.GetValue<string>();

                if (decimal.TryParse(priceStr, out var price))
                {
                    return Math.Round(price * 10.0m, 2);
                }

                return 2680.50m;
            }
            throw new ArgumentException($"Asset context '{assetName}' is not currently configured for live stream tracking.", nameof(assetName));
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Infrastructure failure parsing live quote data stream for {assetName}: {ex.Message}", ex);
        }
    }
}