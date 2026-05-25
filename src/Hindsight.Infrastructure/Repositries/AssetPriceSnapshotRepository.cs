using Hindsight.Application.interfaces;
using Hindsight.Domain.Entities;
using Hindsight.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Hindsight.Infrastructure.Repositories;

public class AssetPriceSnapshotRepository : IAssetPriceSnapshot
{
    private readonly AppDbContext _context;

    public AssetPriceSnapshotRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AssetPriceSnapshot?> GetAssetPriceSnapshotAsync(string assetName, string assetType, int year)
    {
        string nameLower = assetName.ToLower().Trim();
        string typeLower = assetType.ToLower().Trim();

        return await _context.AssetPriceSnapshots
            .AsNoTracking()
            .FirstOrDefaultAsync(e => 
                e.AssetName.ToLower() == nameLower && 
                e.AssetType.ToLower() == typeLower && 
                e.Year == year);
    }
}