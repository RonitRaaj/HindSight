using Hindsight.Application.DTO;
using Hindsight.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hindsight.WebAPI.Controllers;

[ApiController]
[Route("api/analytics")] 
public class HindsightController : ControllerBase
{
    private readonly CalculatorService _calculatorService;
    private readonly MetadataService _metaDataService;

    public HindsightController(CalculatorService calculatorService , MetadataService metaDataService)
    {
        _calculatorService = calculatorService;
        _metaDataService = metaDataService;
    }

    [HttpPost("calculate")] 
    public async Task<IActionResult> GetUserResponse([FromBody] UserRequestDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _calculatorService.GetMissedOpportunity(dto);
            
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected server error occurred bud.", Details = ex.Message });
        }
    }

    [HttpGet("meta-lookup")]
    public async Task<IActionResult> GetMetaData()
    {
        return Ok(await _metaDataService.GetMetaData());
    }
}