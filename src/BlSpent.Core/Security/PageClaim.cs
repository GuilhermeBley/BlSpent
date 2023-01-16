using System.Security.Claims;

namespace BlSpent.Core.Security;

/// <summary>
/// Page claims
/// </summary>
public static class PageClaim
{
    /// <summary>
    /// PageRule
    /// </summary>
    public const string Type = nameof(PageClaim);

    /// <summary>
    /// Available roles
    /// </summary>
    public static IEnumerable<string> AvailableRoles =>
        new Claim[] { Owner, ReadOnly, Modifier }.Select(claim => claim.Value);

    /// <summary>
    /// UserClaim | Owner
    /// </summary>
    public static Claim Owner { get; } = new Claim(Type, "Owner");

    /// <summary>
    /// UserClaim | ReadOnly
    /// </summary>
    public static Claim ReadOnly { get; } = new Claim(Type, "ReadOnly");

    /// <summary>
    /// UserClaim | Modifier
    /// </summary>
    public static Claim Modifier { get; } = new Claim(Type, "Modifier");
}