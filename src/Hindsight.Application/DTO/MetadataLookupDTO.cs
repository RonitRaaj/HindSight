namespace Hindsight.Application.DTO;
public record MetadataLookupDTO(
    IDictionary<string, IEnumerable<string>> CategorizedAssets,
    IEnumerable<string> Currencies
);