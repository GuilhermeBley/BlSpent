using System.Security.Claims;

namespace BlSpent.Core.Security;

/// <summary>
/// Page claims
/// </summary>
public static class PageClaimId
{
    /// <summary>
    /// PageRule
    /// </summary>
    public const string Type = nameof(PageClaimId);

    /// <summary>
    /// Create with pageId
    /// </summary>
    /// <param name="pageId">page id</param>
    /// <returns>Claim with page id</returns>
    public static Claim Create(string pageId)
    {
        if (string.IsNullOrEmpty(pageId))
            pageId = string.Empty;
            
        return new Claim(Type, pageId);
    }
}