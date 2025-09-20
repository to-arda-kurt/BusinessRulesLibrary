using BRL.Core.Interfaces;
using BRL.Core.Models;

namespace BRL.Library.Rules;

public enum ComparisonOperator
{
    LessThan,
    LessThanOrEqual,
    Equal,
    GreaterThanOrEqual,
    GreaterThan,
    Between
}

public class AssetValueRule : IRule
{
    private readonly ComparisonOperator _operator;
    private readonly double _threshold1;
    private readonly double _threshold2;
    private readonly bool _usePrimaryAsset;
    private readonly string _ruleCode;

    public AssetValueRule(
        ComparisonOperator comparisonOperator,
        double threshold1,
        double threshold2 = 0,
        bool usePrimaryAsset = true,
        string ruleCode = "AssetValueRule")
    {
        _operator = comparisonOperator;
        _threshold1 = threshold1;
        _threshold2 = threshold2;
        _usePrimaryAsset = usePrimaryAsset;
        _ruleCode = ruleCode;
    }

    public RuleResult Evaluate(MemberDataContext context)
    {
        double assetValue = _usePrimaryAsset ? context.PrimaryAssetValue : context.SecondaryAssetValue;
        string assetType = _usePrimaryAsset ? "Primary" : "Secondary";
        
        bool isEligible = _operator switch
        {
            ComparisonOperator.LessThan => assetValue < _threshold1,
            ComparisonOperator.LessThanOrEqual => assetValue <= _threshold1,
            ComparisonOperator.Equal => Math.Abs(assetValue - _threshold1) < 0.001,
            ComparisonOperator.GreaterThanOrEqual => assetValue >= _threshold1,
            ComparisonOperator.GreaterThan => assetValue > _threshold1,
            ComparisonOperator.Between => assetValue >= _threshold1 && assetValue <= _threshold2,
            _ => false
        };

        string message = GetResultMessage(isEligible, assetValue, assetType);
        
        return new RuleResult(isEligible, _ruleCode, message);
    }

    private string GetResultMessage(bool isEligible, double assetValue, string assetType)
    {
        if (isEligible)
        {
            return $"{assetType} asset value (£{assetValue:N2}) meets requirements.";
        }
        
        return _operator switch
        {
            ComparisonOperator.LessThan => $"{assetType} asset value (£{assetValue:N2}) is not less than £{_threshold1:N2}.",
            ComparisonOperator.LessThanOrEqual => $"{assetType} asset value (£{assetValue:N2}) exceeds maximum £{_threshold1:N2}.",
            ComparisonOperator.Equal => $"{assetType} asset value (£{assetValue:N2}) is not equal to £{_threshold1:N2}.",
            ComparisonOperator.GreaterThanOrEqual => $"{assetType} asset value (£{assetValue:N2}) is below minimum £{_threshold1:N2}.",
            ComparisonOperator.GreaterThan => $"{assetType} asset value (£{assetValue:N2}) is not greater than £{_threshold1:N2}.",
            ComparisonOperator.Between => $"{assetType} asset value (£{assetValue:N2}) is not between £{_threshold1:N2} and £{_threshold2:N2}.",
            _ => $"{assetType} asset value (£{assetValue:N2}) does not meet requirements."
        };
    }
}