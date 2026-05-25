using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hindsight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialHistoricalTimeline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetPriceSnapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssetType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    AssetName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceAtYear = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetPriceSnapshots", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AssetPriceSnapshots",
                columns: new[] { "Id", "AssetName", "AssetType", "PriceAtYear", "Year" },
                values: new object[,]
                {
                    { new Guid("00d072c5-e8d7-be3c-9fef-3b87900ca726"), "Silver", "Commodity", 4.88m, 2003 },
                    { new Guid("07f3680c-1910-c7ed-3553-f718260c0cd9"), "Silver", "Commodity", 19.08m, 2014 },
                    { new Guid("0a30117c-0298-9d49-1c17-c3864086e150"), "Gold", "Commodity", 444.74m, 2005 },
                    { new Guid("0a7fd477-3165-c423-3319-f2809d66cc5d"), "Apple", "Stock", 1.67m, 2005 },
                    { new Guid("0b226bb2-1913-f4f3-754c-4db07b8015ba"), "Netflix", "Stock", 720.00m, 2025 },
                    { new Guid("0b229771-f64a-c152-34c1-d0ea13f4eb48"), "Gold", "Commodity", 1668.98m, 2012 },
                    { new Guid("0e42faa1-0433-9de8-89fa-ddcf3d3686d2"), "Gold", "Commodity", 279.10m, 2000 },
                    { new Guid("0fff1b80-585a-e4f3-0f39-6f8d003b5aff"), "Apple", "Stock", 4.58m, 2007 },
                    { new Guid("1620d3c5-e003-da6a-ee7b-31344a9aeb2c"), "Netflix", "Stock", 480.20m, 2020 },
                    { new Guid("16450fdb-b1d8-5a0a-a822-4af22fbaba57"), "Bitcoin", "Crypto", 28543.00m, 2023 },
                    { new Guid("1b4ee125-e573-afda-2efd-03350d85aaf7"), "Silver", "Commodity", 32.20m, 2025 },
                    { new Guid("1c179a44-09dd-4d7c-3a4a-b37cdc2d83b3"), "Bitcoin", "Crypto", 526.00m, 2014 },
                    { new Guid("1f2d16ec-407a-e3fb-e7f2-8653030f04fa"), "Silver", "Commodity", 4.37m, 2001 },
                    { new Guid("212c421f-4942-330d-21c7-4ca3c919446c"), "Netflix", "Stock", 3.40m, 2006 },
                    { new Guid("248e1472-5c6e-2177-70ba-af863245021d"), "Apple", "Stock", 13.00m, 2011 },
                    { new Guid("2555b708-5a6b-b94b-f86f-90659fbc423f"), "Gold", "Commodity", 309.73m, 2002 },
                    { new Guid("2653d078-931a-4173-20bc-92cccf37b2e8"), "Apple", "Stock", 2.52m, 2006 },
                    { new Guid("266ce76c-0679-843c-9367-cd394d270217"), "Gold", "Commodity", 363.38m, 2003 },
                    { new Guid("290bdb40-cff5-a6e9-b24b-a465604f222e"), "Gold", "Commodity", 1268.49m, 2018 },
                    { new Guid("2b8565b5-8768-6bdd-5f09-23431e18106f"), "Netflix", "Stock", 0.50m, 2002 },
                    { new Guid("36a713aa-8b1d-1dd0-e1fb-8d945ebecd04"), "Bitcoin", "Crypto", 95000.00m, 2025 },
                    { new Guid("3aa1f044-0d09-9046-c608-568211e5b9ba"), "Netflix", "Stock", 4.10m, 2008 },
                    { new Guid("3ab77047-8ae1-b48b-4116-1a8bd3a8b626"), "Netflix", "Stock", 3.10m, 2007 },
                    { new Guid("3b743308-8611-3876-5107-24a4f99a65f5"), "Apple", "Stock", 52.09m, 2019 },
                    { new Guid("41c22e7f-211e-ddad-0506-0d266d038856"), "Apple", "Stock", 16.88m, 2013 },
                    { new Guid("4477de1d-936d-f548-2e94-5ed9ce0afe96"), "Gold", "Commodity", 1798.61m, 2021 },
                    { new Guid("4509aa5c-2d4c-69a1-805f-57e3155e0b5b"), "Netflix", "Stock", 16.80m, 2010 },
                    { new Guid("493346d5-19df-bb5c-3094-9f8de7e379e3"), "Silver", "Commodity", 17.04m, 2017 },
                    { new Guid("4e4db56f-499c-b84b-b6d1-bd5c2eedebe8"), "Gold", "Commodity", 1248.07m, 2016 },
                    { new Guid("50785246-3180-551f-33e6-e5b133d54010"), "Silver", "Commodity", 4.95m, 2000 },
                    { new Guid("507960b1-cc55-946f-7a78-39ac8ee1da1d"), "Netflix", "Stock", 102.10m, 2016 },
                    { new Guid("50f30032-fe8d-0477-1b18-a3844c093871"), "Gold", "Commodity", 1160.06m, 2015 },
                    { new Guid("51704b1e-507f-799a-2fe3-c82d1a4dab35"), "Apple", "Stock", 180.00m, 2024 },
                    { new Guid("5186612f-ddc6-8b98-50ad-92269eab81ed"), "Apple", "Stock", 5.07m, 2008 },
                    { new Guid("51c55874-73d5-b4b6-26df-95d527dd2a21"), "Apple", "Stock", 0.33m, 2003 },
                    { new Guid("52052cac-9b9f-c117-1ea1-84b908a87eb1"), "Netflix", "Stock", 11.90m, 2012 },
                    { new Guid("54daf955-489e-2e09-d4ca-d7703be22e9a"), "Bitcoin", "Crypto", 28198.00m, 2022 },
                    { new Guid("55c43713-16ca-7208-a19a-73645fa758a2"), "Netflix", "Stock", 290.10m, 2022 },
                    { new Guid("55e2e399-9bd5-3a27-9cd7-d0ded54ac4c5"), "Silver", "Commodity", 13.38m, 2007 },
                    { new Guid("55f50372-fda7-6713-20a5-ca330181a182"), "Bitcoin", "Crypto", 65000.00m, 2024 },
                    { new Guid("5b06394d-32b5-e8ee-5298-912a8f465dd7"), "Bitcoin", "Crypto", 4000.00m, 2017 },
                    { new Guid("5e00f243-3a59-5b0c-7e15-fdb9ec850ab8"), "Apple", "Stock", 20.64m, 2012 },
                    { new Guid("609ff569-61d3-401f-6a04-0ad7a7d76872"), "Netflix", "Stock", 63.10m, 2014 },
                    { new Guid("6161a52c-149a-016d-6009-6f0311e376ca"), "Gold", "Commodity", 1571.52m, 2011 },
                    { new Guid("621c05d2-e54f-ccd9-57de-3365c6da338f"), "Apple", "Stock", 0.61m, 2004 },
                    { new Guid("642b5991-ad88-d050-8e07-5fb2555fe6ea"), "Gold", "Commodity", 1411.23m, 2013 },
                    { new Guid("65a51355-37ee-146a-1f67-5c562eaaab6d"), "Apple", "Stock", 9.28m, 2010 },
                    { new Guid("672f86a4-35c2-28e8-f48d-9f39086c1b69"), "Netflix", "Stock", 580.50m, 2021 },
                    { new Guid("67d49196-62b7-0a96-476c-7f8b2865eab7"), "Apple", "Stock", 154.51m, 2022 },
                    { new Guid("6b9cdf43-82a1-f896-aecb-9fca76185643"), "Silver", "Commodity", 21.76m, 2022 },
                    { new Guid("6bbe5209-c307-1643-f442-d6db4a94927e"), "Bitcoin", "Crypto", 47417.00m, 2021 },
                    { new Guid("6c099dc9-8c89-e890-c7fa-0bafc4099f9e"), "Bitcoin", "Crypto", 7193.00m, 2019 },
                    { new Guid("74012ca9-d060-3298-3e7e-4333d9255247"), "Netflix", "Stock", 1.20m, 2003 },
                    { new Guid("7569ec0b-bb56-8886-ea8c-cc2969d2de0d"), "Bitcoin", "Crypto", 567.00m, 2016 },
                    { new Guid("78ec88a3-b4fc-f5dc-f88c-91412628e8c0"), "Apple", "Stock", 47.27m, 2018 },
                    { new Guid("7b0a8148-c504-87f5-449d-005cc17f410b"), "Silver", "Commodity", 35.12m, 2011 },
                    { new Guid("7fcfcbb9-826e-f62f-4c81-e4633baa429b"), "Apple", "Stock", 26.15m, 2016 },
                    { new Guid("80c66de0-b5dc-35e3-3797-a400d1a88647"), "Netflix", "Stock", 310.20m, 2018 },
                    { new Guid("81d78ad6-d42f-91cd-a300-f5c71c267c75"), "Silver", "Commodity", 4.60m, 2002 },
                    { new Guid("85927da0-a498-917f-e1cc-80e641c6bc9e"), "Bitcoin", "Crypto", 0.10m, 2010 },
                    { new Guid("860ec988-095b-328a-a5ff-b312c69787ef"), "Silver", "Commodity", 28.50m, 2024 },
                    { new Guid("88a08cac-7a08-7825-8b32-b4995fe799ea"), "Bitcoin", "Crypto", 5.50m, 2011 },
                    { new Guid("8bee5c71-d83f-ecf0-2494-6869856cf82c"), "Netflix", "Stock", 6.30m, 2009 },
                    { new Guid("8eaca92e-002a-1321-246f-8ee09a8cde59"), "Gold", "Commodity", 271.04m, 2001 },
                    { new Guid("8efad27f-023b-62f6-7889-637d9413c093"), "Gold", "Commodity", 2350.00m, 2024 },
                    { new Guid("9199fc68-07da-177b-75d3-53c3ee702893"), "Silver", "Commodity", 15.71m, 2018 },
                    { new Guid("92361cdc-4672-9cbc-aad1-a833c309bc13"), "Apple", "Stock", 37.64m, 2017 },
                    { new Guid("97584059-547e-9c57-87f3-4343dc71283e"), "Netflix", "Stock", 610.00m, 2024 },
                    { new Guid("9e9227d3-cbc7-cc9c-1abd-19019be8f309"), "Gold", "Commodity", 972.35m, 2009 },
                    { new Guid("a0f6df0f-b0cc-1ac7-496f-5057ea2b52cc"), "Silver", "Commodity", 11.55m, 2006 },
                    { new Guid("a19cdd30-c76b-b346-77a9-efc45a83671d"), "Silver", "Commodity", 14.99m, 2008 },
                    { new Guid("a1e2e521-c838-8d79-06f6-33a6d0b79d8e"), "Gold", "Commodity", 871.96m, 2008 },
                    { new Guid("a581e4ee-d97e-6685-b0fa-704621534ce1"), "Silver", "Commodity", 20.55m, 2020 },
                    { new Guid("a6a3caee-1275-ebdf-ee28-18379c668ce7"), "Silver", "Commodity", 23.35m, 2023 },
                    { new Guid("a8a23041-22e0-1a6b-0e43-2fbc2408eae4"), "Apple", "Stock", 1.00m, 2000 },
                    { new Guid("b2ac5c45-5bc1-295e-a3cc-d78565cb7c2c"), "Apple", "Stock", 23.05m, 2014 },
                    { new Guid("b2b6ee68-3e38-3758-fb5c-03c983166686"), "Gold", "Commodity", 2650.00m, 2025 },
                    { new Guid("b368bea0-e46a-6257-8209-7558e9c903e4"), "Gold", "Commodity", 1224.52m, 2010 },
                    { new Guid("b5f4986a-e5b1-0594-ebc9-0ebc7d0f7a0b"), "Apple", "Stock", 5.23m, 2009 },
                    { new Guid("b711ae1c-10de-32ae-6798-ebd7813f636f"), "Netflix", "Stock", 35.30m, 2013 },
                    { new Guid("b8746e24-ee12-65eb-a2fc-e02c3d687cf4"), "Bitcoin", "Crypto", 11116.00m, 2020 },
                    { new Guid("bc2fda87-3501-3cab-8e14-a6eb2dfb4a02"), "Apple", "Stock", 27.31m, 2015 },
                    { new Guid("bdba3c42-1106-dd8b-ab3b-32a0a2edc5bb"), "Apple", "Stock", 95.34m, 2020 },
                    { new Guid("c29c2535-a6a4-3331-1ba6-48510c5ccb2d"), "Silver", "Commodity", 31.15m, 2012 },
                    { new Guid("c4f538df-6fbb-00de-ffbf-20a656fceb5e"), "Silver", "Commodity", 17.14m, 2016 },
                    { new Guid("c502de75-fa05-d444-a49c-3551b3d0a445"), "Silver", "Commodity", 15.68m, 2015 },
                    { new Guid("c6b9c556-2cb5-ed46-f04e-724e05313801"), "Apple", "Stock", 0.26m, 2002 },
                    { new Guid("cc6ba934-032f-c9dc-6f68-a39e07b50eb9"), "Bitcoin", "Crypto", 7558.00m, 2018 },
                    { new Guid("cc73c274-17e9-b49d-14c5-8af4a6a3f833"), "Gold", "Commodity", 1257.15m, 2017 },
                    { new Guid("cddf03b1-4da9-7746-6a99-02234bc1e7cc"), "Netflix", "Stock", 165.40m, 2017 },
                    { new Guid("ce4aa45e-e908-7afd-e586-54fabfb408ed"), "Bitcoin", "Crypto", 8.20m, 2012 },
                    { new Guid("ce764ee1-7255-0196-c634-7cc7557ae5f6"), "Gold", "Commodity", 1266.40m, 2014 },
                    { new Guid("cece7303-9aac-f4b7-25e2-4202362653db"), "Bitcoin", "Crypto", 272.00m, 2015 },
                    { new Guid("cf0678a4-8a6f-e394-7624-6730b9012d24"), "Silver", "Commodity", 6.67m, 2004 },
                    { new Guid("d2fd1577-7f20-5efd-ec49-4aae0c90a7a3"), "Apple", "Stock", 220.00m, 2025 },
                    { new Guid("d41ccb06-f425-8646-2a32-3f92c7499359"), "Netflix", "Stock", 325.90m, 2019 },
                    { new Guid("d42a77c6-4f58-4fb3-b4b8-8375b72554a4"), "Gold", "Commodity", 695.39m, 2007 },
                    { new Guid("d58cbc07-9efe-d76a-d1d3-d95c695a1241"), "Netflix", "Stock", 27.20m, 2011 },
                    { new Guid("d79cc4e1-a031-f080-3b35-9b0d7cd37e55"), "Silver", "Commodity", 14.67m, 2009 },
                    { new Guid("d9a67152-49f3-9885-d32c-45d966a61f5b"), "Apple", "Stock", 0.39m, 2001 },
                    { new Guid("e172cabb-6bc2-a6b0-7680-305fa298c535"), "Gold", "Commodity", 1940.54m, 2023 },
                    { new Guid("e19aaaaa-6a42-e91d-d13f-06e13bec62c2"), "Gold", "Commodity", 1769.64m, 2020 },
                    { new Guid("e2c6ed24-f161-7123-c990-634d227d1af5"), "Silver", "Commodity", 23.79m, 2013 },
                    { new Guid("e355976b-65ef-6edb-803e-3f9ab3522a93"), "Apple", "Stock", 140.98m, 2021 },
                    { new Guid("e4684a08-f144-a068-139b-1bbd77e25cf3"), "Gold", "Commodity", 409.72m, 2004 },
                    { new Guid("e7cfaeb4-4160-4674-4d55-d281e10693a6"), "Gold", "Commodity", 603.46m, 2006 },
                    { new Guid("ee74dbf4-8ae8-2122-2887-2fdbd616ebac"), "Silver", "Commodity", 25.14m, 2021 },
                    { new Guid("eec5916d-d2c7-9bfb-3aa0-1e8322210042"), "Gold", "Commodity", 1392.60m, 2019 },
                    { new Guid("ef295e5c-b23c-6458-39e7-8facd8c35938"), "Netflix", "Stock", 91.80m, 2015 },
                    { new Guid("efc02c6e-79b8-d38d-d1f6-0ea36a86b365"), "Apple", "Stock", 172.62m, 2023 },
                    { new Guid("f209fe49-811b-ec2d-8020-a8bf815f94ab"), "Netflix", "Stock", 1.95m, 2005 },
                    { new Guid("f6daa409-0ea0-444b-eee9-53bac641b869"), "Silver", "Commodity", 16.21m, 2019 },
                    { new Guid("fabd2bd9-c8cb-006a-bb03-0b6ec76a45e5"), "Bitcoin", "Crypto", 189.54m, 2013 },
                    { new Guid("faef70b1-5739-a2b7-bd66-50c2ed19993a"), "Silver", "Commodity", 7.32m, 2005 },
                    { new Guid("fbc5a668-e980-11e2-1acd-e663f5a13bab"), "Netflix", "Stock", 410.80m, 2023 },
                    { new Guid("fe87f20b-03fa-fb3f-796f-800eb45717e5"), "Silver", "Commodity", 20.19m, 2010 },
                    { new Guid("ff53a303-1598-f921-1902-2d00093f323c"), "Gold", "Commodity", 1800.09m, 2022 },
                    { new Guid("ffaa90c5-4038-1a33-f093-6c27ea835643"), "Netflix", "Stock", 1.80m, 2004 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetPriceSnapshots");
        }
    }
}
