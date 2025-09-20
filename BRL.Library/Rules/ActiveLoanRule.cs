using BRL.Core.Interfaces;
using BRL.Core.Models;

namespace BRL.Library.Rules;

public class ActiveLoanRule : IRule
{
    public RuleResult Evaluate(MemberDataContext context)
    {
        bool isEligible = !context.HasActiveLoan;
        return new RuleResult(isEligible, "ActiveLoanStatus",
            isEligible ? "Member has no active loans." : "Member has one or more active loans.");
    }
}