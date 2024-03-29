using BlSpent.Core.Security;
using System.Security.Claims;

namespace BlSpent.Application.Model;

/// <summary>
/// Model of claims
/// </summary>
public class ClaimModel
{
    public static ClaimModel Default { get; } = new();

    /// <summary>
    /// UserId
    /// </summary>
    public Guid? UserId { get; } = null;

    /// <summary>
    /// Unique email
    /// </summary>
    public string? Email { get; } = null;

    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; } = null;

    /// <summary>
    /// Name
    /// </summary>
    public string? LastName { get; } = null;

    /// <summary>
    /// Page Id
    /// </summary>
    public Guid? PageId { get; } = null;

    /// <summary>
    /// Type of access to page
    /// </summary>
    public string? AccessType { get; } = null;

    /// <summary>
    /// Token expires date
    /// </summary>
    public DateTime? Expires { get; } = null;

    /// <summary>
    /// Check if is a invite to page
    /// </summary>
    public bool IsInvite { get; }

    /// <summary>
    /// Check if a request to change the password
    /// </summary>
    public bool IsNotRememberPassword { get; }

    private ClaimModel() { }

    public ClaimModel(
        Guid? userId, string? email, string? name, string? lastName, Guid? pageId, 
        string? accessType, DateTime? expires, bool isInvite = false, bool isNotRememberPassword = false)
    {
        UserId = userId;
        Name = name;
        LastName = lastName;
        Email = email;
        PageId = pageId;
        AccessType = accessType;
        Expires = expires;
        IsInvite = isInvite;
        IsNotRememberPassword = isNotRememberPassword;
    }
}