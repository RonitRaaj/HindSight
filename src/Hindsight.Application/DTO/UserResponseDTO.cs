namespace Hindsight.Application.DTO;

public record UserResponseDTO(

    decimal price,
    decimal missedProfit,
    string currency,
    string message
);