using System.Security.Claims;

namespace BlSpent.Core.Security;

/// <summary>
/// Page claims
/// </summary>
public static class PageInvite
{
    /// <summary>
    /// PageRule
    /// </summary>
    public const string Type = nameof(PageInvite);

    /// <summary>
    /// Create with pageId
    /// </summary>
    /// <param name="ownerInvite">owner of invite</param>
    /// <returns>Claim with owner of invite</returns>
    public static Claim Create(string ownerInvite)
    {
        if (string.IsNullOrEmpty(ownerInvite))
            ownerInvite = string.Empty;
            
        return new Claim(Type, ownerInvite);
    }
}