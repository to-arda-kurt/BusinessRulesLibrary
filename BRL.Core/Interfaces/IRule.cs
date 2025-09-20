using BRL.Core.Models;
using System.Text.Json.Serialization;

namespace BRL.Core.Interfaces;

public interface IRule
{
    RuleResult Evaluate(MemberDataContext context);
}

public class RuleResult
{
    [JsonPropertyName("isEligible")]
    public bool IsEligible { get; }
    
    [JsonPropertyName("ruleName")]
    public string RuleCode { get; }
    
    [JsonPropertyName("note")]
    public string Message { get; }
    
    public object? Payload { get; }

    public RuleResult(bool isEligible, string ruleCode, string message, object? payload = null)
    {
        IsEligible = isEligible;
        RuleCode = ruleCode;
        Message = message;
        Payload = payload;
    }
}