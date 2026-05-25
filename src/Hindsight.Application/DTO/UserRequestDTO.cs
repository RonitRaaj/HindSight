using System.ComponentModel.DataAnnotations;

namespace Hindsight.Application.DTO;

public record UserRequestDTO
(
    [Required(ErrorMessage ="AssetType is Required")]
    string AssetType ,

    [Required(ErrorMessage ="AssetType is Required")]
    string AssetName,

    [Required(ErrorMessage ="Year is required")]
    [Range(1990,2025,ErrorMessage = "Year is out of range")]
    int Year,

    [Required(ErrorMessage = "Price you wanted to Invested is required.")]
    [Range(0.0, double.MaxValue, ErrorMessage = "Price cannot be negative.")]
    decimal InvestedPrice,

    [Required(ErrorMessage = "Currency is required.")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be a 3-letter ISO code (e.g., INR, USD, EUR).")]
    string Currency
);