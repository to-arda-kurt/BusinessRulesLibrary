using BRL.Api.Models;
using BRL.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace BRL.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EligibilityController : ControllerBase
{
    private readonly ClaimValidator _claimValidator;

    public EligibilityController(ClaimValidator claimValidator)
    {
        _claimValidator = claimValidator;
    }

    [HttpPost]
    public IActionResult CheckEligibility([FromBody] EligibilityRequest request)
    {
        if (request?.MemberData == null)
        {
            return BadRequest("Member data is required.");
        }

        if (request.ClaimsToCheck == null || !request.ClaimsToCheck.Any())
        {
            return BadRequest("At least one claim type must be specified.");
        }

        var results = _claimValidator.Evaluate(request.MemberData, request.ClaimsToCheck);
        return Ok(results);
    }
}