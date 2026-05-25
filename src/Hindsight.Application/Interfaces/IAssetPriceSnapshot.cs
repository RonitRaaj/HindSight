using Hindsight.Domain.Entities;

namespace Hindsight.Application.interfaces;

public interface IAssetPriceSnapshot
{
    Task<AssetPriceSnapshot?> GetAssetPriceSnapshotAsync(string assetName , string assetType , int year);
}