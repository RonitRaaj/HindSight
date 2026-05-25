namespace Hindsight.Application.Interfaces;

public interface IAssetPriceFetcher
{
    Task<decimal> GetLivePriceAsync(string assetName, CancellationToken cancellationToken = default);
}