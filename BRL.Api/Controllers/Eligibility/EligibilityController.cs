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
        if (request?.MemberData == null || request.ClaimsToCheck == null)
        {
            return BadRequest("Request is missing required data.");
        }
        var results = _claimValidator.Evaluate(request.MemberData, request.ClaimsToCheck);
        return Ok(results);
    }
}