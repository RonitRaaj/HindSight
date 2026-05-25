using Hindsight.Application.DTO;
using Hindsight.Application.Interfaces;

namespace Hindsight.Application.Services;

public class MetadataService
{
    private readonly IMetadataLookup _metaDataLookup;

    public MetadataService(IMetadataLookup metadataLookup)
    {
        _metaDataLookup = metadataLookup;
    }

    public async Task<MetadataLookupDTO> GetMetaData()
    {
        var supportedCurrencies = new List<string> 
        { 
            "USD", "INR", "EUR", "GBP", "CAD", "AUD", "JPY", "AED", "SGD" 
        };

        return await _metaDataLookup.GetMetaData();
    }
    
}
