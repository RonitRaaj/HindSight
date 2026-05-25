using Hindsight.Application.DTO;
using Hindsight.Application.interfaces;
using Hindsight.Application.Interfaces;

namespace Hindsight.Application.Services;

public class CalculatorService
{
    private readonly IAssetPriceSnapshot _assetPriceSnapshot;
    private readonly IAssetPriceFetcher _assetPriceFetcher;
    private readonly ICurrencyExchange _currencyExchange;

    public CalculatorService(IAssetPriceSnapshot assetPriceSnapshot, IAssetPriceFetcher assetPriceFetcher, ICurrencyExchange currencyExchange)
    {
        _assetPriceSnapshot = assetPriceSnapshot;
        _assetPriceFetcher = assetPriceFetcher;
        _currencyExchange = currencyExchange;
    }

    public async Task<UserResponseDTO> GetMissedOpportunity(UserRequestDTO dTO)
    {
        string normalizedName = dTO.AssetName.ToLowerInvariant().Trim();
        string normalizedType = dTO.AssetType.ToLowerInvariant().Trim();

        // 1. Life-cycle Guard Rails
        if (normalizedName == "bitcoin" && dTO.Year < 2010)
        {
            throw new InvalidOperationException("Bitcoin was not invented before 2010 smartass");
        }

        if (normalizedName == "netflix" && dTO.Year < 2002)
        {
            throw new InvalidOperationException("Netflix was not listed before 2002 smartass");
        }

        var asset = await _assetPriceSnapshot.GetAssetPriceSnapshotAsync(normalizedName, normalizedType, dTO.Year);
        if (asset == null)
        {
            throw new InvalidOperationException($"No historical data found for {dTO.AssetName} in {dTO.Year}. Something is wrong bud!!");
        }

        decimal toUsdRate = await _currencyExchange.GetExchangeRateAsync(dTO.Currency, "USD");
        decimal investmentInUsd = dTO.InvestedPrice * toUsdRate;

        var assetUnitsOwned = investmentInUsd / asset.PriceAtYear;

        var currentPriceOfAssetInUsd = await _assetPriceFetcher.GetLivePriceAsync(normalizedName);

        decimal fromUsdRate = await _currencyExchange.GetExchangeRateAsync("USD", dTO.Currency);


        var totalCurrentValueLocal = currentPriceOfAssetInUsd * assetUnitsOwned * fromUsdRate;
        var missedOpportunityLocal = totalCurrentValueLocal - dTO.InvestedPrice;

        var roundedPrice = Math.Round(totalCurrentValueLocal, 2);
        var roundedMissedProfit = Math.Round(missedOpportunityLocal < 0 ? 0 : missedOpportunityLocal, 2);

        decimal growthMultiplier = dTO.InvestedPrice > 0
            ? totalCurrentValueLocal / dTO.InvestedPrice
            : 0;

        string punchline;

        if (growthMultiplier >= 10.0m)

        {
            punchline = $"A massive {Math.Round(growthMultiplier, 0)}x return?! Absolute brutality. Now go cry in your basement.";
        }
        else if (growthMultiplier >= 3.0m)
        {
            punchline = $"More than a {Math.Round(growthMultiplier, 0)}x gain missed. Ouch, that's gonna sting a bit.";
        }
        else if (growthMultiplier >= 1.1m)
        {
            punchline = $"{Math.Round(growthMultiplier, 0)}x gain missed,it's fine you will survive.";
        }
        else if (growthMultiplier > 0.0m && growthMultiplier < 1.1m)
        {
            punchline = $"{Math.Round(growthMultiplier, 0)}x gain missed Basically broke even. You're fine.";
        }
        else
        {
            punchline = "The asset actually lost value. You dodged a bullet, absolute legend!";
        }

        return new UserResponseDTO(
            price: roundedPrice,
            missedProfit: roundedMissedProfit,
            currency: dTO.Currency,
            message : punchline
        );
    }
}