using BRL.Core.Enums;

namespace BRL.Business.Models;

public record ClaimValidationResult
{
    public ClaimType ClaimType { get; init; }
    public bool IsEligible { get; init; }
    public List<string> Notes { get; } = new();
}