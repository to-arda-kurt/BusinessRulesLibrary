using BRL.Core.Enums;
using BRL.Core.Interfaces;
using BRL.Library.Rules;

namespace BRL.Business.Services;

public static class RuleRegistry
{
    private static readonly Dictionary<ClaimType, IEnumerable<IRule>> RulesByClaimType;

    static RuleRegistry()
    {
        var isLongTermMember = new IsLongTermMemberRule();
        var age18to35 = new MemberAgeRule(18, 35);

        RulesByClaimType = new Dictionary<ClaimType, IEnumerable<IRule>>
        {
            [ClaimType.LongTermBenefit] = new List<IRule> { isLongTermMember },
            [ClaimType.YoungFamilySupport] = new List<IRule> { age18to35, isLongTermMember },
        };
    }

    public static IEnumerable<IRule> GetRulesFor(ClaimType claimType) => 
        RulesByClaimType.TryGetValue(claimType, out var rules) ? rules : Enumerable.Empty<IRule>();
}