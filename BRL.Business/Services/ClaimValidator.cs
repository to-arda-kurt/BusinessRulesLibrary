using BRL.Core.Enums;
using BRL.Core.Interfaces;
using BRL.Core.Models;
using BRL.Business.Models;

namespace BRL.Business.Services;

public class ClaimValidator
{
    public ClaimValidationResult ValidateClaim(ClaimType claimType, MemberDataContext memberData)
    {
        var rules = RuleRegistry.GetRulesFor(claimType);
        
        var failedRules = new List<RuleResult>();
        var notes = new List<string>();
        
        foreach (var rule in rules)
        {
            var result = rule.Evaluate(memberData);
            if (!result.IsMatch) 
            {
                failedRules.Add(result);
                notes.Add(result.Note);
            }
        }
        
        bool isEligible = !failedRules.Any();
        
        return new ClaimValidationResult
        {
            ClaimType = claimType,
            IsEligible = isEligible,
            FailedRules = failedRules
        };
    }
}