using BRL.Core.Interfaces;
using BRL.Core.Models;

namespace BRL.Library.Rules;

public class MemberAgeRule : IRule
{
    private readonly int _minAge;
    private readonly int _maxAge;

    public MemberAgeRule(int minAge, int maxAge)
    {
        _minAge = minAge;
        _maxAge = maxAge;
    }

    public RuleResult Evaluate(MemberDataContext context)
    {
        bool isEligible = context.Age >= _minAge && context.Age <= _maxAge;
        return new RuleResult(isEligible, "AgeRestriction",
            isEligible ? $"Age ({context.Age}) is within range." : $"Age ({context.Age}) is outside range.");
    }
}