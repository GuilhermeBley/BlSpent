using System.Security.Claims;

namespace BlSpent.Core.Security;

/// <summary>
/// Page claims
/// </summary>
public static class PageClaims
{
    /// <summary>
    /// PageRule
    /// </summary>
    public const string Type = nameof(PageClaims);

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