using BRL.Core.Enums;
using BRL.Core.Interfaces;
using BRL.Library.Rules;

namespace BRL.Business.Services;

public static class RuleRegistry
{
    private static readonly Dictionary<ClaimType, IEnumerable<IRule>> RulesByClaimType;

    static RuleRegistry()
    {
        // Common rules
        var isLongTermMember = new IsLongTermMemberRule();
        var age18to35 = new MemberAgeRule(18, 35);
        var age25Plus = new MemberAgeMinimumRule(25);
        var noPendingClaims = new PendingClaimRule();
        var noOverduePayment = new OverduePaymentRule();
        
        // Use the factory to create asset rules for each claim type, it's for value based rules
        var claimOneAssetRule = AssetRuleFactory.CreateAssetRuleForClaimType(ClaimType.ClaimOne);
        var claimTwoAssetRule = AssetRuleFactory.CreateAssetRuleForClaimType(ClaimType.ClaimTwo);
        var claimThreeAssetRule = AssetRuleFactory.CreateAssetRuleForClaimType(ClaimType.ClaimThree);
        var claimFourAssetRule = AssetRuleFactory.CreateAssetRuleForClaimType(ClaimType.ClaimFour);
        
        // Optional secondary asset rule
        var secondaryAssetRule = new AssetValueRule(
            ComparisonOperator.LessThanOrEqual,
            5000,
            usePrimaryAsset: false,
            ruleCode: "SecondaryAssetLimit");

        RulesByClaimType = new Dictionary<ClaimType, IEnumerable<IRule>>
        {
            [ClaimType.ClaimOne] = new List<IRule> { 
                isLongTermMember,
                claimOneAssetRule,
                noPendingClaims
            },
            
            [ClaimType.ClaimTwo] = new List<IRule> { 
                age18to35, 
                isLongTermMember,
                claimTwoAssetRule
            },
            
            [ClaimType.ClaimThree] = new List<IRule> {
                age25Plus,
                claimThreeAssetRule,
                secondaryAssetRule
            },
            
            [ClaimType.ClaimFour] = new List<IRule> {
                claimFourAssetRule,
                new ActiveLoanRule(),
                noOverduePayment
            }
        };
    }

    public static IEnumerable<IRule> GetRulesFor(ClaimType claimType) => 
        RulesByClaimType.TryGetValue(claimType, out var rules) ? rules : Enumerable.Empty<IRule>();
        
    public static IEnumerable<IRule> GetDynamicRulesFor(ClaimType claimType)
    {
        var commonRules = new List<IRule> { new OverduePaymentRule() };
        
        var claimSpecificRules = claimType switch
        {
            ClaimType.ClaimOne => new List<IRule> { 
                new IsLongTermMemberRule(),
                new PendingClaimRule()
            },
            
            ClaimType.ClaimTwo => new List<IRule> { 
                new MemberAgeRule(18, 35) 
            },
            
            ClaimType.ClaimThree => new List<IRule> { 
                new MemberAgeMinimumRule(25),
                new AssetValueRule(
                    ComparisonOperator.LessThanOrEqual,
                    5000,
                    usePrimaryAsset: false)
            },
            
            _ => new List<IRule>()
        };
        
        // Add asset rule using factory
        try
        {
            var assetRule = AssetRuleFactory.CreateAssetRuleForClaimType(claimType);
            return commonRules
                .Concat(claimSpecificRules)
                .Append(assetRule);
        }
        catch (ArgumentException)
        {
            // Factory doesn't support this claim type, return without asset rule
            return commonRules.Concat(claimSpecificRules);
        }
    }
}