namespace Hindsight.Domain.Entities;

public class AssetPriceSnapshot
{
    public Guid Id { get; init; }
    public string AssetType { get; private set; }
    public string AssetName { get; private set; }
    public int Year { get; private set; }
    public decimal PriceAtYear { get; private set; }
    private AssetPriceSnapshot()
    {
        Id = Guid.Empty;
        AssetType = null!;
        AssetName = null!;
    }

    public AssetPriceSnapshot(string assetType, string assetName, int year, decimal priceAtYear)
    {
        if (string.IsNullOrWhiteSpace(assetType))
        {
            throw new ArgumentException("Asset Type cannot be empty.", nameof(assetType));
        }
        
        if (string.IsNullOrWhiteSpace(assetName))
        {
            throw new ArgumentException("Asset Name cannot be empty.", nameof(assetName));
        }
        
        if (year < 2000 || year > 2025)
        {
            throw new ArgumentOutOfRangeException(nameof(year), "Year must be between 2000 and 2025 inclusive.");
        }
        
        if (priceAtYear < 0)
        {
            throw new ArgumentException("Price cannot be negative.", nameof(priceAtYear));
        }

        Id = Guid.NewGuid();
        AssetType = assetType;
        AssetName = assetName;
        Year = year;
        PriceAtYear = priceAtYear;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
        {
            throw new ArgumentException("Price cannot be negative.", nameof(newPrice));
        }
        PriceAtYear = newPrice;
    }
}