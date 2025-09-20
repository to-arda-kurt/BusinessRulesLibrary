using Microsoft.AspNetCore.Mvc;
using BRL.Business.Services;
using BRL.Api.Models;
using BRL.Core.Models;
using BRL.Core.Enums;
using System;

namespace BRL.Api.Controllers.Eligibility;

[ApiController]
[Route("api/[controller]")]
public class EligibilityController : ControllerBase
{
    private readonly ClaimValidator _claimValidator;
    
    public EligibilityController(ClaimValidator claimValidator)
    {
        _claimValidator = claimValidator;
    }
    
    [HttpPost("check")]
    public IActionResult CheckEligibility(EligibilityRequest request)
    {
        if (request.MemberData == null)
            return BadRequest("Member data is required");
            
        // If no claim types provided, check eligibility for all claim types
        IEnumerable<ClaimType> claimTypesToCheck = request.ClaimsToCheck;
        if (claimTypesToCheck == null || !claimTypesToCheck.Any())
        {
            claimTypesToCheck = Enum.GetValues(typeof(ClaimType))
                                     .Cast<ClaimType>();
        }
        
        var results = new List<object>();
        
        foreach (var claimType in claimTypesToCheck)
        {
            var result = _claimValidator.ValidateClaim(claimType, request.MemberData);
            results.Add(result);
        }
        
        return Ok(new { 
            MemberId = request.MemberData.MemberId,
            Results = results 
        });
    }
}