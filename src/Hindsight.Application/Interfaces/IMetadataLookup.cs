using Hindsight.Application.DTO;

namespace Hindsight.Application.Interfaces;

public interface IMetadataLookup
{
    Task<MetadataLookupDTO> GetMetaData();
}