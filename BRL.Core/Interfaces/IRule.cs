using BRL.Core.Models;

namespace BRL.Core.Interfaces;

public record RuleResult(bool IsMatch, string ClaimType, string Note, object? Payload = null);

public interface IRule
{
    RuleResult Evaluate(MemberDataContext context);
}