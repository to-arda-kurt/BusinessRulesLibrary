using BRL.Core.Enums;
using BRL.Core.Models;

namespace BRL.Api.Models;

public class EligibilityRequest
{
    public MemberDataContext? MemberData { get; set; }
    public List<ClaimType>? ClaimsToCheck { get; set; }
}