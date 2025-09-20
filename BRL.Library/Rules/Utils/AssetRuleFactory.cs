using BRL.Core.Enums;
using BRL.Core.Interfaces;

namespace BRL.Library.Rules;


public class AssetRuleFactory
{
    public static IRule CreateAssetRuleForClaimType(ClaimType claimType)
    {
        return claimType switch
        {
            ClaimType.ClaimOne => new AssetValueRule(
                ComparisonOperator.GreaterThan, 
                10000, 
                ruleCode: "StandardClaimAssetRule"),
                
            ClaimType.ClaimTwo => new AssetValueRule(
                ComparisonOperator.LessThan, 
                2000, 
                ruleCode: "EmergencyClaimAssetRule"),
                
            ClaimType.ClaimThree => new AssetValueRule(
                ComparisonOperator.Between, 
                20000, 
                100000, 
                ruleCode: "PremiumClaimAssetRule"),
                
            ClaimType.ClaimFour => new AssetValueRule(
                ComparisonOperator.GreaterThanOrEqual, 
                50, 
                ruleCode: "MicroLoanAssetRule"),
                
            _ => throw new ArgumentException($"Unsupported claim type: {claimType}")
        };
    }
}