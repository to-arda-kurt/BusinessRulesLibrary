using BRL.Core.Interfaces;
using BRL.Core.Models;

namespace BRL.Library.Rules;

public class PendingClaimRule : IRule
{
    public RuleResult Evaluate(MemberDataContext context)
    {
        bool isEligible = !context.IsMemberHasPendingClaim;
        return new RuleResult(isEligible, "PendingClaimStatus",
            isEligible ? "Member has no pending claims." : "Member has one or more pending claims.");
    }
}