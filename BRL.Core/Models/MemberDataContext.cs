using System.Diagnostics.Contracts;

namespace BRL.Core.Models;

public class MemberDataContext
{
    public int MemberId { get; set; }
    public int Age { get; set; }
    public DateTime MemberSince { get; set; }
    public double PrimaryAssetValue { get; set; }
    public double SecondaryAssetValue { get; set; }
    public bool HasActiveLoan { get; set; }
    public bool HasDelinquentLoan { get; set; }
    public bool IsPep { get; set; }
    public bool IsMemberRequestedMoneyThisMonth { get; set; }
    public bool IsMemberHasAnotherActiveClaim { get; set; }
    public bool IsMemberHasPendingClaim { get; set; }
    public bool IsMemberHasOverduePayment { get; set; } 
}