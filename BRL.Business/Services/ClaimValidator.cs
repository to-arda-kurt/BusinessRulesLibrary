using BRL.Business.Models;
using BRL.Core.Enums;
using BRL.Core.Models;

namespace BRL.Business.Services;

public class ClaimValidator
{
    public ClaimValidationResult Evaluate(MemberDataContext context, ClaimType claimType)
    {
        var rulesForClaim = RuleRegistry.GetRulesFor(claimType).ToList();
        var validationResult = new ClaimValidationResult { ClaimType = claimType };
        var allRulesMatch = rulesForClaim.Any(); 

        foreach (var rule in rulesForClaim)
        {
            var result = rule.Evaluate(context);
            validationResult.Notes.Add(result.Note);
            if (!result.IsMatch) allRulesMatch = false;
        }
        
        return validationResult with { IsEligible = allRulesMatch };
    }

    public List<ClaimValidationResult> Evaluate(MemberDataContext context, IEnumerable<ClaimType> claimTypes) =>
        claimTypes.Select(claim => Evaluate(context, claim)).ToList();
}