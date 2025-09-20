using BRL.Core.Enums;
using BRL.Core.Interfaces;

namespace BRL.Business.Models;

public record ClaimValidationResult
{
    public ClaimType ClaimType { get; init; }
    public bool IsEligible { get; init; }
    public List<string> Notes { get; } = new();
    public List<RuleResult> FailedRules { get; init; } = new();
}