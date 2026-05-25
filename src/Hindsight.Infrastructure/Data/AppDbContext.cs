using Hindsight.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Hindsight.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<AssetPriceSnapshot> AssetPriceSnapshots { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AssetPriceSnapshot>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AssetName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.AssetType).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Year).IsRequired();
            entity.Property(e => e.PriceAtYear).IsRequired().HasColumnType("decimal(18,2)");
        });

        SeedHistoricalData(modelBuilder);
    }


    private static void SeedHistoricalData(ModelBuilder modelBuilder)
    {
        var snapshots = new List<object>();

        // =========================================================================
        // 1. COMMODITY: GOLD (Exact historical USD closing price per ounce)
        // =========================================================================
        var goldPrices = new Dictionary<int, decimal>
        {
            { 2000, 279.10m }, { 2001, 271.04m }, { 2002, 309.73m }, { 2003, 363.38m },
            { 2004, 409.72m }, { 2005, 444.74m }, { 2006, 603.46m }, { 2007, 695.39m },
            { 2008, 871.96m }, { 2009, 972.35m }, { 2010, 1224.52m }, { 2011, 1571.52m },
            { 2012, 1668.98m }, { 2013, 1411.23m }, { 2014, 1266.40m }, { 2015, 1160.06m },
            { 2016, 1248.07m }, { 2017, 1257.15m }, { 2018, 1268.49m }, { 2019, 1392.60m },
            { 2020, 1769.64m }, { 2021, 1798.61m }, { 2022, 1800.09m }, { 2023, 1940.54m },
            { 2024, 2350.00m }, { 2025, 2650.00m }
        };

        foreach (var kvp in goldPrices)
        {
            snapshots.Add(new
            {
                Id = CreateDeterministicGuid($"Commodity-Gold-{kvp.Key}"),
                AssetType = "Commodity",
                AssetName = "Gold",
                Year = kvp.Key,
                PriceAtYear = kvp.Value
            });
        }

        // =========================================================================
        // 2. COMMODITY: SILVER (Historical USD closing price per ounce)
        // =========================================================================
        var silverPrices = new Dictionary<int, decimal>
        {
            { 2000, 4.95m },   { 2001, 4.37m },   { 2002, 4.60m },   { 2003, 4.88m },
            { 2004, 6.67m },   { 2005, 7.32m },   { 2006, 11.55m },  { 2007, 13.38m },
            { 2008, 14.99m },  { 2009, 14.67m },  { 2010, 20.19m },  { 2011, 35.12m },
            { 2012, 31.15m },  { 2013, 23.79m },  { 2014, 19.08m },  { 2015, 15.68m },
            { 2016, 17.14m },  { 2017, 17.04m },  { 2018, 15.71m },  { 2019, 16.21m },
            { 2020, 20.55m },  { 2021, 25.14m },  { 2022, 21.76m },  { 2023, 23.35m },
            { 2024, 28.50m },  { 2025, 32.20m }
        };

        foreach (var kvp in silverPrices)
        {
            snapshots.Add(new
            {
                Id = CreateDeterministicGuid($"Commodity-Silver-{kvp.Key}"),
                AssetType = "Commodity",
                AssetName = "Silver",
                Year = kvp.Key,
                PriceAtYear = kvp.Value
            });
        }

        // =========================================================================
        // 3. STOCK: APPLE (Split-adjusted baseline values)
        // =========================================================================
        var applePrices = new Dictionary<int, decimal>
        {
            { 2000, 1.00m },  { 2001, 0.39m },  { 2002, 0.26m },  { 2003, 0.33m },
            { 2004, 0.61m },  { 2005, 1.67m },  { 2006, 2.52m },  { 2007, 4.58m },
            { 2008, 5.07m },  { 2009, 5.23m },  { 2010, 9.28m },  { 2011, 13.00m },
            { 2012, 20.64m }, { 2013, 16.88m }, { 2014, 23.05m }, { 2015, 27.31m },
            { 2016, 26.15m }, { 2017, 37.64m }, { 2018, 47.27m }, { 2019, 52.09m },
            { 2020, 95.34m }, { 2021, 140.98m }, { 2022, 154.51m }, { 2023, 172.62m },
            { 2024, 180.00m }, { 2025, 220.00m }
        };

        foreach (var kvp in applePrices)
        {
            snapshots.Add(new
            {
                Id = CreateDeterministicGuid($"Stock-Apple-{kvp.Key}"),
                AssetType = "Stock",
                AssetName = "Apple",
                Year = kvp.Key,
                PriceAtYear = kvp.Value
            });
        }

        // =========================================================================
        // 4. STOCK: NETFLIX (IPO in 2002; split-adjusted approximations)
        // =========================================================================
        var netflixPrices = new Dictionary<int, decimal>
        {
            { 2002, 0.50m },  { 2003, 1.20m },  { 2004, 1.80m },  { 2005, 1.95m },
            { 2006, 3.40m },  { 2007, 3.10m },  { 2008, 4.10m },  { 2009, 6.30m },
            { 2010, 16.80m }, { 2011, 27.20m }, { 2012, 11.90m }, { 2013, 35.30m },
            { 2014, 63.10m }, { 2015, 91.80m }, { 2016, 102.10m }, { 2017, 165.40m },
            { 2018, 310.20m }, { 2019, 325.90m }, { 2020, 480.20m }, { 2021, 580.50m },
            { 2022, 290.10m }, { 2023, 410.80m }, { 2024, 610.00m }, { 2025, 720.00m }
        };

        foreach (var kvp in netflixPrices)
        {
            snapshots.Add(new
            {
                Id = CreateDeterministicGuid($"Stock-Netflix-{kvp.Key}"),
                AssetType = "Stock",
                AssetName = "Netflix",
                Year = kvp.Key,
                PriceAtYear = kvp.Value
            });
        }

        // =========================================================================
        // 5. CRYPTO: BITCOIN (Launches in 2009; mapped from 2010 onwards)
        // =========================================================================
        var bitcoinPrices = new Dictionary<int, decimal>
        {
            { 2010, 0.10m },   { 2011, 5.50m },    { 2012, 8.20m },    { 2013, 189.54m },
            { 2014, 526.00m }, { 2015, 272.00m },  { 2016, 567.00m },  { 2017, 4000.00m },
            { 2018, 7558.00m }, { 2019, 7193.00m }, { 2020, 11116.00m }, { 2021, 47417.00m },
            { 2022, 28198.00m }, { 2023, 28543.00m }, { 2024, 65000.00m }, { 2025, 95000.00m }
        };

        foreach (var kvp in bitcoinPrices)
        {
            snapshots.Add(new
            {
                Id = CreateDeterministicGuid($"Crypto-Bitcoin-{kvp.Key}"),
                AssetType = "Crypto",
                AssetName = "Bitcoin",
                Year = kvp.Key,
                PriceAtYear = kvp.Value
            });
        }

        modelBuilder.Entity<AssetPriceSnapshot>().HasData(snapshots);
    }

    private static Guid CreateDeterministicGuid(string input)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = MD5.HashData(inputBytes);
        return new Guid(hashBytes);
    }
}