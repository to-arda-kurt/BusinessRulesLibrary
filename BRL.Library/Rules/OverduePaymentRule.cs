using BRL.Core.Interfaces;
using BRL.Core.Models;

namespace BRL.Library.Rules;

public class OverduePaymentRule : IRule
{
    public RuleResult Evaluate(MemberDataContext context)
    {
        bool isEligible = !context.IsMemberHasOverduePayment;
        return new RuleResult(isEligible, "OverduePaymentStatus",
            isEligible ? "Member has no overdue payments." : "Member has overdue payments.");
    }
}