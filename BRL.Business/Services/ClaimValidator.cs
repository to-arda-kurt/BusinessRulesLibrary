using BRL.Core.Enums;
using BRL.Core.Interfaces;
using BRL.Core.Models;
using BRL.Business.Models;
using System.Data;

namespace BRL.Business.Services;

public class ClaimValidator
{
    public ClaimValidationResult ValidateClaim(ClaimType claimType, MemberDataContext memberData)
    {
        var rules = RuleRegistry.GetRulesFor(claimType);
        //var rules2 = RuleRegistry.GetDynamicRulesFor(claimType);
        
        var failedRules = new List<RuleResult>();
        var notes = new List<string>();
        
        foreach (var rule in rules)
        {
            var result = rule.Evaluate(memberData);
            if (!result.IsEligible) 
            {
                failedRules.Add(result);
                notes.Add(result.Message);
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