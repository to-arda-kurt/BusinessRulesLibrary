using BRL.Core.Interfaces;
using BRL.Core.Models;

namespace BRL.Rules.Rules;

public class IsLongTermMemberRule : IRule
{
    public RuleResult Evaluate(MemberDataContext context)
    {
        bool isEligible = context.MemberSince <= DateTime.UtcNow.AddYears(-1);
        return new RuleResult(isEligible, "LongTermBenefit", 
            isEligible ? "Member is active for over one year." : "Member must be active for at least one year.");
    }
}