using Hindsight.Application.DTO;
using Hindsight.Application.Interfaces;
using Hindsight.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Hindsight.Infrastructure.Repositories;

public class MetadataLookupRepository : IMetadataLookup
{
    private readonly AppDbContext _context;

    public MetadataLookupRepository(AppDbContext context){
        _context = context;
    }

    public async Task<MetadataLookupDTO> GetMetaData()
    {
        var rawAssets = await _context.AssetPriceSnapshots
            .AsNoTracking()
            .Select(x => new { x.AssetType, x.AssetName })
            .Distinct()
            .ToListAsync();

        var categorized = rawAssets
            .GroupBy(x => x.AssetType)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.AssetName).OrderBy(name => name).AsEnumerable()
            );


        var supportedCurrencies = new List<string> { "USD", "INR", "EUR", "GBP", "CAD", "AUD", "JPY", "AED", "SGD" };

        return new MetadataLookupDTO(
            CategorizedAssets: categorized,
            Currencies: supportedCurrencies.OrderBy(c => c)
        );
    }
}