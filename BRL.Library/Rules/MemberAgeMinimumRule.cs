using BRL.Core.Interfaces;
using BRL.Core.Models;

namespace BRL.Library.Rules;

public class MemberAgeMinimumRule : IRule
{
    private readonly int _minAge;

    public MemberAgeMinimumRule(int minAge = 18)
    {
        _minAge = minAge;
    }

    public RuleResult Evaluate(MemberDataContext context)
    {
        bool isEligible = context.Age >= _minAge;
        return new RuleResult(isEligible, "MinimumAgeRequirement",
            isEligible ? $"Member meets minimum age requirement ({context.Age} >= {_minAge})." : 
                         $"Member does not meet minimum age requirement ({context.Age} < {_minAge}).");
    }
}